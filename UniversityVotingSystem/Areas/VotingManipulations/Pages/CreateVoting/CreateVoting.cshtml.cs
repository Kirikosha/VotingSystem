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
    [IgnoreAntiforgeryToken]
    public class CreateVotingModel : PageModel
    {
        public IPropositionVotingRepository _repository {get; set;}
        private IJsonParser _jsonParser;
        public CreateVotingModel(IPropositionVotingRepository repository)
        {
            _repository = repository;
            _jsonParser = new JsonParserVoting();
        }

        public void OnGet()
        {
            Console.WriteLine("It is get new");
        }

        public IActionResult OnPost()
        {
            ParsedJsonResult? parsedRequest = _jsonParser.ParseJsonString(Request, Encoding.UTF8).Result as ParsedJsonResult;
            if(parsedRequest is null)
            {
                throw new ArgumentNullException(nameof(parsedRequest), "Request was not parsed well. It is null");
            }
            VotingAndProposotionFactory factory = new VotingAndProposotionFactory(_repository);
            bool isOperationSuccessful = factory.InsertVotingAndPropositions(parsedRequest);
            if (isOperationSuccessful)
            {
                return new StatusCodeResult(200);
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
