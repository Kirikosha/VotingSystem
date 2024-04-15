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

            bool isOperationSuccessful = CreateAndInsertVoting(parsedRequest.name).Result;
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

        private async Task<bool> CreateAndInsertVoting(string votingName)
        {
            bool isPresent = await repository_.isPresent(votingName);
            if (isPresent)
            {
                return false;
            }
            Voting voting = new Voting{voting_name = votingName};
            bool result = repository_.CreateVoting(voting);
            return result;
        }
    }
}
