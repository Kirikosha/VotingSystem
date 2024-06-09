using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using UniversityVotingSystem.Models;

namespace UniversityVotingSystem.webpages
{
    [IgnoreAntiforgeryToken]
    public class AllVotingsModel : PageModel
    {
        public List<ProposotionsPerVotingObject> propPerVotingObjectList {get; set;}
        IPropositionVotingRepository _propositionVotingRepository;
        public AllVotingsModel (IPropositionVotingRepository repository)
        {
            _propositionVotingRepository = repository;
            propPerVotingObjectList = new List<ProposotionsPerVotingObject>();
        }

        public IActionResult OnGet()
        {
            List<Voting> votings =_propositionVotingRepository.GetAllVotings().Result.ToList();
            propPerVotingObjectList = new List<ProposotionsPerVotingObject>(votings.Count());
            int index = 0;
            foreach (Voting voting in votings)
            {
                List<Proposition> propositions = _propositionVotingRepository.GetAllPropositionsById(voting.voting_id).Result.ToList();
                ProposotionsPerVotingObject propPerVotingObj = new ProposotionsPerVotingObject(voting, propositions);


                propPerVotingObjectList.Add(propPerVotingObj);
                index++;
            }

            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            int votingId;
            string jsonString;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                jsonString = await reader.ReadToEndAsync();
                if(string.IsNullOrEmpty(jsonString))
                {
                    throw new ArgumentNullException(nameof(jsonString));
                }

                JObject jsonObject = JObject.Parse(jsonString);
                JToken? idValue;
                bool isGettingValueSuccessful = jsonObject.TryGetValue("id", out idValue);
                if(idValue is null){
                    throw new ArgumentNullException(nameof(idValue));
                }

                bool success = Int32.TryParse(idValue.ToString(), out votingId);
                if(!success){
                    return BadRequest();
                }
            }
            if(DeleteVoting(votingId).Result){
                return OnGet();
            }
            return BadRequest();
        }

        private async Task<bool> DeleteVoting(int votingId){
            Voting voting = await _propositionVotingRepository.GetVotingById(votingId);
            bool result = _propositionVotingRepository.DeleteVoting(voting);
            return result;
        }
    }
}