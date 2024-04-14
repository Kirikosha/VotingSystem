function createProposition(){
    var propositionContainer = document.getElementById("create-voting");
    var propositionHTMLCollection = document.getElementsByClassName("voting-propositions");
    var lastIndexVar = "proposition-" + lastIndex(propositionHTMLCollection);

    //Div and input initialization
    var innerDiv = document.createElement("div");
    innerDiv.className = "voting-propositions";
    innerDiv.id = lastIndexVar;
    var innerInput = document.createElement("input");
    innerInput.type = "text";
    innerInput.placeholder = "Proposition name";

    innerDiv.appendChild(innerInput);
    propositionContainer.appendChild(innerDiv);
}

function lastIndex(collection){
    var lastElement = collection[collection.length - 1];
    var idString = lastElement.getAttribute("id");
    var splittedId = idString.split('-');
    var id = Number(splittedId[splittedId.length - 1]);
    if(id != NaN){
        return id+1;
    }
    return 2;
}

function deleteLastProposition(){
    var propositionHTMLCollection = document.getElementsByClassName("voting-propositions");
    var lastIndexVar = lastIndex(propositionHTMLCollection) - 1;
    if(lastIndexVar > 1){
        var index = "proposition-" + lastIndexVar;
        document.getElementById(index).remove();
    }
    else{
        alert("You can't delete that last one proposition, it hast to be at least 1 in one voitng!!!");
    }
}

function submitVoting(){
    const propositionHTMLCollection = document.getElementsByClassName("voting-propositions");
    const propositionArray = Array.from(propositionHTMLCollection);
    var propositionValues = filterPropositions(propositionArray);
    var votingName = document.getElementById("votingName").value;
    console.log(votingName);
    var jsonString = getJson(votingName, propositionValues);
    console.log(jsonString);
    makePostRequest(jsonString);
}

function filterPropositions(propositionArray){
    var values = new Array();
    for(i = 0; i < propositionArray.length; i++){
        var inputValue = propositionArray[i].children[0].value;
        if(inputValue.length >= 1){
            values.push(inputValue);
        }
    }
    return values;
}

function getJson(votingName, propositionValues){
    
    var jsonObj = {
        name : votingName,
        propositions : propositionValues
    };
    var jsonString = JSON.stringify(jsonObj);
    return jsonString;
}

function makePostRequest(jsonString){
    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body : jsonString
    };
    fetch('/CreateVoting', requestOptions)
    .then(response => response.json())
    .then(data =>{
        console.log('Response from server: ', data);
    });
}

function getToken(){
    var token = '@Html.AntiForgeryToken()';
    token = $(token).val();
    return token;
}