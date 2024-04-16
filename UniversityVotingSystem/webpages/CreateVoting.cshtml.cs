using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UniversityVotingSystem.Models;
using UniversityVotingSystem.Repository;

namespace UniversityVotingSystem.webpages
{
    public class CreateVotingModel : PageModel
    {
        public IDataBaseRepository repository_ {get; set;}
        public CreateVotingModel(IDataBaseRepository repository)
        {
            repository_ = repository;
        }
        public sealed class PostRq
        {
            public string name {get; set;}
            public string[] propositions {get; set;}
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            PostRq parsedRequest = ParseJsonString().Result;

            if (string.IsNullOrWhiteSpace(parsedRequest.name) || string.IsNullOrWhiteSpace(parsedRequest.propositions[0]))
            {
                return RedirectToPage("/error");
            }

            bool isOperationSuccessful = InsertVotingAndPropositions(parsedRequest).Result;
            if (isOperationSuccessful)
            {
                Console.WriteLine("Success");
                return new PageResult();
            }
            else
            {
                Console.WriteLine("Bad request");
                return BadRequest();
            }


        }

        private async Task<PostRq> ParseJsonString(){
            PostRq parsedRequest = new PostRq();
            string jsonString;

            //Parsing the body of request to get values
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                jsonString = await reader.ReadToEndAsync();
                parsedRequest = JsonConvert.DeserializeObject<PostRq>(jsonString);
            }
            return parsedRequest;
        }

        private async Task<Voting> CreateVoting(string votingName)
        {
            bool isPresent = await repository_.isPresent(votingName);
            if (isPresent)
            {
                return null;
            }
            Voting voting = new Voting{voting_name = votingName};
            return voting;
        }

        private Proposition[] MapPropositions(string[] propositions)
        {
            Proposition[] propositionsArray = new Proposition[propositions.Length];
            for (int i = 0; i < propositions.Length; i++)
            {
                propositionsArray[i] = new Proposition{proposition_text = propositions[i]};
            }
            return propositionsArray;
        }

        private async Task<bool> InsertVotingAndPropositions(PostRq data)
        {
            Voting voting = await CreateVoting(data.name);
            if (voting is null)
            {
                return false;
            }
            voting.Propositions = MapPropositions(data.propositions).ToList();
            bool isDbRequestSuccessful = repository_.CreateVoting(voting);
            return isDbRequestSuccessful ? true : false;
        }    
    }
}
