using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniversityVotingSystem.webpages
{
    public class CreateVotingModel : PageModel
    {
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

            foreach(string proposition in parsedRequest.propositions){
                Console.WriteLine("proposition : " + proposition);
            }
            return new PageResult();
        }

        private async Task<PostRq> ParseJsonString(){
            PostRq parsedRequest = new PostRq();
                        string jsonString;
            //Parsing the body of request to get values
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8)){
                jsonString = await reader.ReadToEndAsync();
                parsedRequest = JsonConvert.DeserializeObject<PostRq>(jsonString);
                if(parsedRequest != null){
                }
            }
            return parsedRequest;
        }

        
    }
}
