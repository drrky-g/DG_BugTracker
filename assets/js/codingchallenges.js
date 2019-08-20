//************************************
//// Coding Challenges
//*************************************


//code display toggles
$("#code-display-fb").hide();
$('#code-display-math').hide();
$('#code-display-fact').hide();
$('#code-display-soaf').hide();
$('#code-display-pal').hide();

$("#btnToggleMath").click(function () {
    $("#code-display-math").toggle();

    if ($(this).text() == "Show Code") {
        $(this).text("Hide Code");
    } else {
        $(this).text("Show Code");
    }
})

$("#btnToggleFact").click(function () {
    $("#code-display-fact").toggle();

    if ($(this).text() == "Show Code") {
        $(this).text("Hide Code");
    } else {
        $(this).text("Show Code");
    }
})

$("#btnToggleSOAF").click(function () {
    $("#code-display-soaf").toggle();

    if ($(this).text() == "Show Code") {
        $(this).text("Hide Code");
    } else {
        $(this).text("Show Code");
    }
})

$("#btnTogglePal").click(function () {
    $("#code-display-pal").toggle();

    if ($(this).text() == "Show Code") {
        $(this).text("Hide Code");
    } else {
        $(this).text("Show Code");
    }
})

$("#btnToggleFB").click(function () {
    $("#code-display-fb").toggle();

    if ($(this).text() == "Show Code") {
        $(this).text("Hide Code");
    } else {
        $(this).text("Show Code");
    }
})



//Sum of All Fears Scripts
//submit button listener
$("#fearSubmit").click(function () {


    //store inputs
    var indexZero = +$("#index0").val();
    var indexOne = +$("#index1").val();
    var indexTwo = +$("#index2").val();
    var indexThree = +$("#index3").val();
    var indexFour = +$("#index4").val();
    var kValue = +$("#kvalue").val();

    console.log('Values stored.');
    console.log('K is...');
    console.log(kValue);

    //push values into array
    var sOAFears = [
        indexZero,
        indexOne,
        indexTwo,
        indexThree,
        indexFour
    ];


    console.log('Array Built')
    console.log(sOAFears);

    for (var i = 0; i < 4; i++) {
        //going through array and taking final value for comparing sums
        var pulledNumber = sOAFears.pop();
        console.log('The array just popped ' + pulledNumber + ' to compare.')
        //remaining array values shown in console
        console.log('Your remaining array values are:')
        console.log(sOAFears);
        //add pulled value to remaining array indexes
        console.log('Adding pulled number to array:')
        var modifiedArray = sOAFears.map(addPulled)

        function addPulled(num) {
            return num + pulledNumber;

        }

        console.log(modifiedArray);

        //find funct
        function doesKExist(modifiedArray) {
            return modifiedArray == kValue;
        }

        //store sum being searched in variable
        var kWasFound = modifiedArray.find(doesKExist);
        console.log(kWasFound);


        if (kWasFound === kValue) {
            break;
        }

    }
    //show result in output field
    if (kValue === kWasFound) {
        $("#koutput").text("K Exists!")
        console.log("K Exists!")
    } else {
        $("#koutput").text("K was not found.")
        console.log("K is not found.")
    }


})

//clear button
$("#fearClear").click(function () {
    $("#index0").val('');
    $("#index1").val('');
    $("#index2").val('');
    $("#index3").val('');
    $("#index4").val('');
    $("#kvalue").val('');
    $("#koutput").text('');

})


//FizzBuzz Scripts

$("#btnFBRun").click(function () {
    //Store Inputs
    var fizzNum = +$("#fizzInput").val();
    var buzzNum = +$("#buzzInput").val();
    var fbNum = fizzNum * buzzNum;
    //Form array
    var outputArray = []
    //fill array 1-100
    for (var i = 1; i < 101; i++) {
        if (i % fbNum == 0) {
            //isolate numbers divided by both
            outputArray.push("<span id='fizzbuzzcolor'>FizzBuzz</span>");
        } else if (i % fizzNum == 0) {
            //isolate numbers divided by fizz
            outputArray.push("<span id='fizzcolor'>Fizz</span>");
        } else if (i % buzzNum == 0) {
            //isolate numbers divided by buzz
            outputArray.push("<span id='buzzcolor'>Buzz</span>");
        } else outputArray.push(i);
    }
    //convert array into string
    var fbArray = outputArray.join(' ');
    //display results
    $("#fbOutput").html(fbArray);






    //logs for testing
    console.log('inputs secured, manipulating data...')
    console.log(fizzNum);
    console.log(buzzNum);
    console.log(fbNum);
    console.log(outputArray);


})

$('#btnFBClear').click(function () {
    $('#fbOutput').text('');
    $('#fizzInput').val('');
    $('#buzzInput').val('');
})

//Factorial Scripts
//Listener for Button Press
$("#startFactor").click(function () {
    //Collect Data
    var factEntry = +$("#factInput").val();
    //Manipulate Data
    var factoredOutput = Factorial(factEntry);
    //Output Manipulated Data
    $("#factOutput").text(factEntry + " has a factorial of " + factoredOutput + ".")
});

//Clear Button Functionality
$("#clearFactor").click(function () {
    $("#factOutput").text('Your factorial will appear here.');
    $("#factInput").val('');
})


//Factorial function being called by factoredOutput variable
function Factorial(num) {
    if (num > 0) {
        for (var loop = num - 1; loop > 1; loop--) {
            num *= loop;
        }
    }
    else if (num == 0) {
        num = 1;
    }
    else {
        num = "undefined";
    }
    return num;

}

//Palindrome Scripts
//Run Button Listener
$("#palBtn").click(function () {
    //Store Input
    var word = $("#palIn").val();
    //Break word into an array of characters
    var wordBreak = word.split('')
    //Flip array
    var flippedArray = wordBreak.reverse();
    //Convert array back into string
    var flippedWord = flippedArray.join('');



    //logs for testing
    console.log('storing data and running methods...')
    console.log(word);
    console.log(wordBreak);
    console.log(flippedArray);
    console.log(flippedWord);



    if (word === flippedWord) {
        $("#palOut").text('Looks like ' + word + ' is a palindrome!')
    } else {
        $("#palOut").text('Unfortunately that is not a palindrome.')
    }
})

//Clear Button Listener
$("#palClear").click(function () {
    $("#palOut").text('');
    $("#palIn").val('')
})

//Basic Math Scripts
//submit button event listener
$("#mathbtn").click(function () {
    //Step 1: Get User Data
    var num1 = +$("#num1").val();
    var num2 = +$("#num2").val();
    var num3 = +$("#num3").val();
    var num4 = +$("#num4").val();
    var num5 = +$("#num5").val();
    //Step 2: Manipulate data with calculations
    var smallest = Math.min(num1, num2, num3, num4, num5);
    var largest = Math.max(num1, num2, num3, num4, num5);
    var sum = num1 + num2 + num3 + num4 + num5;
    var product = num1 * num2 * num3 * num4 * num5;
    var mean = sum / 5;
    //Display Results
    $("#smallest").text("The smallest number in the set is: " + smallest);
    $("#largest").text("The largest number in the set is: " + largest);
    $("#product").text("The product of the numbers is: " + product);
    $("#sum").text("The sum of the numbers is: " + sum);
    $("#mean").text("The average of the numbers in the set is: " + mean);

})
//clear button event listener
$("#mathclear").click(function () {
    $("#num1").val('');
    $("#num2").val('');
    $("#num3").val('');
    $("#num4").val('');
    $("#num5").val('');
    $("#smallest").text('');
    $("#largest").text('');
    $("#product").text('');
    $("#sum").text('');
    $("#mean").text('');
}) 

//Blackjack
//Blackjack Script
// by Derrick Gordon

//Card Variables
let suits = ['Hearts', 'Clubs', 'Diamonds', 'Spades'];
let values = ['Ace', 'King', 'Queen', 'Jack', 'Ten',
    'Nine', 'Eight', 'Seven', 'Six', 'Five', 'Four', 'Three', 'Two'];

//DOM Variables
let textArea = document.getElementById('text-area');
let newGameButton = document.getElementById('new-game-button');
let hitButton = document.getElementById('hit-button');
let stayButton = document.getElementById('stay-button');

//Game Variables
let gameStarted = false,
    gameOver = false,
    playerWon = false,
    dealerCards = [],
    playerCards = [],
    dealerScore = 0,
    playerScore = 0,
    deck = [];

//Hide Hit and Stay on Load
hitButton.style.display = 'none';
stayButton.style.display = 'none';
showStatus();

//Starts Game on 'New Game' Click
newGameButton.addEventListener('click', function () {
    gameStarted = true;
    gameOver = false;
    playerWon = false;
    //Makes Deck
    deck = createDeck();
    //Shuffles Deck
    shuffleDeck(deck);
    //Deals Cards
    dealerCards = [getNextCard(), getNextCard()];
    playerCards = [getNextCard(), getNextCard()];
    //Hides 'New Game' and Shows Controls
    newGameButton.style.display = 'none';
    hitButton.style.display = 'inline';
    stayButton.style.display = 'inline';
    showStatus();
});

hitButton.addEventListener('click', function () {
    playerCards.push(getNextCard());
    checkForEndOfGame();
    showStatus();
});

stayButton.addEventListener('click', function () {
    gameOver = true;
    checkForEndOfGame();
    showStatus();
});

//Builds Deck from 'Suit' and 'Value' Arrays,
function createDeck() {
    let deck = [];
    for (let suitIdx = 0; suitIdx < suits.length; suitIdx++) {
        for (let valueIdx = 0; valueIdx < values.length; valueIdx++) {
            let card = {
                suit: suits[suitIdx],
                value: values[valueIdx]
            };
            deck.push(card);
        }
    }
    return deck;
}

//Deck Shuffle Function
function shuffleDeck(deck) {
    for (let i = 0; i < deck.length; i++) {
        let swapIdx = Math.trunc(Math.random() * deck.length);
        let tmp = deck[swapIdx];
        deck[swapIdx] = deck[i];
        deck[i] = tmp;
    }
}

//Tells you your cards with text
function getCardString(card) {
    return card.value + ' of ' + card.suit;
}
//Draw Card Function
function getNextCard() {
    return deck.shift();
}

function getCardNumericValue(card) {
    switch (card.value) {
        case 'Ace':
            return 1;
        case 'Two':
            return 2;
        case 'Three':
            return 3;
        case 'Four':
            return 4;
        case 'Five':
            return 5;
        case 'Six':
            return 6;
        case 'Seven':
            return 7;
        case 'Eight':
            return 8;
        case 'Nine':
            return 9;
        default:
            return 10;
    }
}

function getScore(cardArray) {
    let score = 0;
    let hasAce = false;
    for (let i = 0; i < cardArray.length; i++) {
        let card = cardArray[i];
        score += getCardNumericValue(card);
        if (card.value === 'Ace') {
            hasAce = true;
        }
    }
    if (hasAce && score + 10 <= 21) {
        return score + 10;
    }
    return score;
}

// Score Tracking Function
function updateScores() {
    dealerScore = getScore(dealerCards);
    playerScore = getScore(playerCards);
}

function checkForEndOfGame() {

    updateScores();

    if (gameOver) {
        //Dealer Draw
        while (dealerScore < playerScore
            && playerScore <= 21
            && dealerScore <= 21) {
            dealerCards.push(getNextCard());
            updateScores();
        }
    }

    if (playerScore > 21) {
        playerWon = false;
        gameOver = true;
    }
    else if (dealerScore > 21) {
        playerWon = true;
        gameOver = true;
    }
    else if (gameOver) {
        if (playerScore > dealerScore) {
            playerWon = true;
        }
        else {
            playerWon = false;
        }
    }
}

//Updates User on Status of Hands & Score
function showStatus() {
    if (!gameStarted) {
        textArea.innerText = 'Welcome to Blackjack.';
        return;
    }

    let dealerCardString = '';
    for (let i = 0; i < dealerCards.length; i++) {
        dealerCardString += getCardString(dealerCards[i]) + '\n';
    }

    let playerCardString = '';
    for (let i = 0; i < playerCards.length; i++) {
        playerCardString += getCardString(playerCards[i]) + '\n';
    }

    updateScores();

    textArea.innerText =
        'Dealer has:\n' +
        dealerCardString +
        '(score: ' + dealerScore + ')\n\n' +

        'Player has:\n' +
        playerCardString +
        '(score: ' + playerScore + ')\n\n'

    if (gameOver) {
        if (playerWon) {
            textArea.innerText += "You Win!";
        }
        else {
            textArea.innerText += "Dealer Wins.";
        }
        newGameButton.style.display = 'inline';
        hitButton.style.display = 'none';
        stayButton.style.display = 'none';
    }
}