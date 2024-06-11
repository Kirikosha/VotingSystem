const SIZE = 3;

function createProposition()
{
    var propositionContainer = document.getElementById("create-voting");
    var propositionHTMLCollection = document.getElementsByClassName("voting-propositions");
    if (lastIndex(propositionHTMLCollection) > 10)
    {
        alert("There can't be > than 10 propositions!!!")
    }
    else
    {
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
}

function lastIndex(collection)
{
    var lastElement = collection[collection.length - 1];
    var idString = lastElement.getAttribute("id");
    var splittedId = idString.split('-');
    var id = Number(splittedId[splittedId.length - 1]);
    if(id != NaN){
        return id+1;
    }
    return 2;
}

function deleteLastProposition()
{
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

function submitVoting()
{
    const propositionHTMLCollection = document.getElementsByClassName("voting-propositions");
    const propositionArray = Array.from(propositionHTMLCollection);
    var propositionValues = filterPropositions(propositionArray);
    var votingName = document.getElementById("votingName").value;
    var stateValue = document.getElementsByName("state").value;
    var startsAt = document.getElementById("startsAt").value;
    var endsAt = document.getElementById("endsAt").value;
    console.log(votingName);
    var isValid = validationStarter(votingName, propositionValues, startsAt, endsAt);
    if (isValid)
    {
        var jsonString = getJson(votingName, propositionValues, stateValue, startsAt, endsAt);
        console.log(jsonString);
        makePostRequest(jsonString);
    }
}

function filterPropositions(propositionArray)
{
    var values = new Array();
    for(i = 0; i < propositionArray.length; i++){
        var inputValue = propositionArray[i].children[0].value;
        if(inputValue.length >= 1){
            values.push(inputValue);
        }
    }
    return values;
}

function getJson(votingName, propositionValues, stateValue, startsAt, endsAt)
{

    var jsonObj = {
        name : votingName,
        propositions : propositionValues,
        state : stateValue,
        starts_at : startsAt,
        ends_at : endsAt
    };
    var jsonString = JSON.stringify(jsonObj);
    return jsonString;
}

function makeGetRequest()
{
    fetch("/AllVotings", {method: "GET"});
}
function makePostRequest(jsonString) {
    var requestOptions = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: jsonString
    };

    fetch('/CreateVoting', requestOptions)
    .then(response => {
        if (response.status === 200 || response.status === 201) {
            console.log('POST request successful.');
            window.location.href = "/All-Votings";
        } else {
            throw new Error('Network response was not ok ' + response.statusText);
        }
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });
}
function validationStarter(votingName, propositionArray, startingDate, endingDate)
{
    var validateVotingNameErrorMessage = validateVotingName(votingName);
    var validatePropositionErrorMessage = validateProposition(propositionArray);
    var validateStartingDateErrorMessage = validateStartingDate(startingDate);
    var validateEndingDateErrorMessage = validateEndingDate(startingDate, endingDate);
    var errorMessage = validateVotingNameErrorMessage + validatePropositionErrorMessage + validateStartingDateErrorMessage + validateEndingDateErrorMessage;
    console.log(errorMessage);
    if(errorMessage != "")
    {
        alert(errorMessage);
        return false; //operation did not pass due to errors
    }
    return true; // operation passed
}

function validateVotingName(votingName)
{
    var errorMessage = "";
    let sizeError = false;
    let capitalLetterError = false;

    if (votingName.length < SIZE)
    {
        sizeError = true;
    }
    
    let firstLetter = votingName.charAt(0);
    if (firstLetter != firstLetter.toUpperCase())
    {
        capitalLetterError = true;
    }

    var errorMessage = createVotingNameErrorMessage(sizeError, capitalLetterError);
    return errorMessage;
}

function validateProposition(propositionArray)
{
    if (propositionArray.length == 0)
    {
        var errorMessage = "\nThere should be at least 1 proposition";
        return errorMessage;
    }
    var propositionsWithErrors = new Array();
    for (var i = 0; i < propositionArray.length; i++)
    {
        if (propositionArray[i].length < SIZE)
        {
            propositionsWithErrors.push(propositionArray[i]);
        }
    }

    var errorMessage = ""; 
    if (propositionsWithErrors.length > 0)
    {
        errorMessage = createPropositionsErrorMessage(propositionsWithErrors);
    }

    return errorMessage;
}

function createPropositionsErrorMessage(propositionsWithErrors)
{
    var errorMessage = "\nYou have written wrong propositions. Their length is smaller than 3. They are:";
    for (var i = 0; i  < propositionsWithErrors.length; i++)
    {
        errorMessage+= "\n" + propositionsWithErrors[i];
    }
    return errorMessage;
}

function createVotingNameErrorMessage(sizeError, capitalLetterError)
{
    if (!sizeError && !capitalLetterError)
    {
        return "";
    }
    var errorMessage = "You have made several mistakes in voting name. They are:";

    if (sizeError)
    {
        errorMessage += "\nSize of voting name should be > 3";
    }

    if (capitalLetterError)
    {
        errorMessage += "\nFirst letter of the voting name should be uppercase";
    }
    return errorMessage;
}

function validateStartingDate(startingDate){
    var inputDate = new Date(startingDate);
    var currentDate = new Date()
    if(inputDate < currentDate){
        return "\nStarting date should not be less than the current on";
    }
    return "";
}

function validateEndingDate(startingDate, endingDate){
    var startingDateObj = new Date(startingDate);
    var endingDateObj = new Date(endingDate)
    if(startingDateObj > endingDateObj){
        return "\nEnding date should be after the starting date, not equal or before";
    }
    return "";
}