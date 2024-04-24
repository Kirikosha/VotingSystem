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
            public string name {get; set;} = new string("");
            public string[]? propositions {get; set;}
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            PostRq parsedRequest = ParseJsonString().Result;
            string? name = parsedRequest.name;
            string[]? propositions = parsedRequest.propositions;
            if (string.IsNullOrWhiteSpace(parsedRequest.name) || propositions is null)
            {
                throw new ArgumentNullException(nameof(parsedRequest), "Either PostRq.name || PostRq.propositions is null");
            }

            bool isOperationSuccessful = InsertVotingAndPropositions(parsedRequest);
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
                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new ArgumentNullException(nameof(jsonString), "JSON can't be parsed");
                }

                parsedRequest = JsonConvert.DeserializeObject<PostRq>(jsonString) ?? new PostRq();
            }

            return parsedRequest;
        }

        private async Task<(Voting, bool)> CreateVoting(string votingName)
        {
            bool isPresent = await repository_.isPresent(votingName);
            if (isPresent)
            {
                return (new Voting(){voting_name = ""}, true) ;
            }

            Voting voting = new Voting{voting_name = votingName};
            return (voting, false);
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

        private bool InsertVotingAndPropositions(PostRq data)
        {
            (Voting, bool) response = CreateVoting(data.name).Result;
            if (response.Item2 is true)
            {
                return false;
            }

            Voting voting = response.Item1;
            if (data.propositions is null)
            {
                throw new ArgumentNullException(nameof(data.propositions), "Propositions were null on the stage of insertion them into db");
            }
            voting.Propositions = MapPropositions(data.propositions).ToList();
            bool isDbRequestSuccessful = repository_.CreateVoting(voting);
            return isDbRequestSuccessful ? true : false;
        }
    }
}
