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
    console.log(propositionValues);
    console.log(propositionArray);
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