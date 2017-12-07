// Sequence Chart - SJG 05Dec2017

const char_A = "A";       // Adenine
const char_C = "C";       // Cytosine
const char_G = "G";       // Guanine
const char_R = "R";       // Purine
const char_T = "T";       // Thymine
const char_U = "U";       // Uracil
const char_Y = "Y";       // Pyrimidine
const char_space = "-";   // Space
const char_other = "*";   // Any other character
const charLookup = ["A", "C", "G", "R", "T", "U", "Y", "-", "*"];
const colorLookup = ["green", "blue", "red", "purple", "orange", "cyan", "gold", "black", "black"];

$(document).ready(function(){
  $('#fileInput').change(generateGraphic);
});

/* Generates and displays a sequence logo based on an input file */
function generateGraphic(e) {
  $('.char-column').remove();                     // Remove previous images
  var file = e.target.files[0];                   // Pull in file
  if (file) {
    var r = new FileReader();
    r.onload = function(e) {
      var content = e.target.result;
      var lines = parseInputData(content);      // Parse file data
      if (lines.length < 1) {
        alert("The provided file does not contain valid sequence data" +
        " for visualization. Please try another file.");
      }
      var ratios = calcCharPercentages(lines);  // Calculate percentages
      drawChart(ratios);                        // Draw figure
      evenOutLines();
      }
      r.readAsText(file);
    }
    else {
      alert("Failed to load file. Please try again.");
    }
  }

  /* Returns array of sequence reads */
  function parseInputData(input) {
    var lines = input.split('\n');
    var dataLines = [];
    for (i = 1; i < lines.length-1; i+=2) {
      dataLines.push(lines[i]);
    }
    return dataLines;
  }

  /* Returns array of arrays. Each sub-array contains the prevalence
     of all characters at a single position. The return array contains
     one entry per position in the final sequence. */
  function calcCharPercentages(data) {
    var numChars = data[0].length;
    var numReads = data.length;

    var finalArr = [];                              // Initialize final array
    for (j = 0; j < numChars; j++) {                // One array per spot
      finalArr.push([0]);
      for (k = 0; k < 9; k++) {                     // One letter per index
        finalArr[j].push(0);
      }
    }

    for (i = 0; i < numReads; i++) {                // Tally counts
      var currSeq = data[i];
      for (j = 0; j < numChars; j++) {
        switch(currSeq.charAt(j).toUpperCase()) {
          case char_A:
            finalArr[j][0]++;
            break;
          case char_C:
            finalArr[j][1]++;
            break;
          case char_G:
            finalArr[j][2]++;
            break;
          case char_R:
            finalArr[j][3]++;
            break;
          case char_T:
            finalArr[j][4]++;
            break;
          case char_U:
            finalArr[j][5]++;
            break;
          case char_Y:
            finalArr[j][6]++;
            break;
          case char_space:
            finalArr[j][7]++;
            break;
          default:
            finalArr[j][8]++;
        }
      }
    }
    var notZeroIndex = 0;
    var rowSum = 0.0;
    for (i = 0; i < numChars; i++) {             // Calculate ratios
      notZeroIndex = 0;
      rowSum = 0;
      for (j = 0; j < 9; j++) {
        finalArr[i][j] = (finalArr[i][j] / numReads) * 20;
        if (finalArr[i][j] > 0) notZeroIndex = j;
        rowSum += finalArr[i][j];
      }
      var diff = 20 - rowSum;                    // Even out borders
      finalArr[i][notZeroIndex] += diff;
    }
    for (k = 0; k < numChars; k++) {              // Add index labels for display
      finalArr[k][9] = k+1;
    }
    return finalArr;
  }

  /* Draws graphic */
  function drawChart(charRatios) {
    d3.select("#sequenceGraphic")
    .selectAll("div")
    .data(charRatios)
      .enter()
      .append("div")
      .attr("class", "char-column")
      .style("margin", "0px")
      .style("float", "left")
      .style("border-style", "solid")
      .style("border-width", "1px")
      .style("position", "relative")
      .style("height", "30%")
      .selectAll("p")
      .data(function(d,i,j) { return d; } )
      .enter()
      .append("p")
      .style("color", function(d,i) { return colorLookup[i]; })
      .style("font-size", function(d,i) { if ((i+1) == charRatios[0].length) { return "20"; } else if (d > 0) { return d + "vh"; } else {return "0"; }})
      .style("position", function(d,i) { if ((i+1) == charRatios[0].length) { return "absolute"; } else { return ""; }})
      .style("bottom", function(d,i) { if ((i+1) == charRatios[0].length) { return "1"; } else { return ""; }})
      .style("right", function(d,i) { if ((i+1) == charRatios[0].length) { return "1"; } else { return ""; }})
      .style("height", function(d,i) { if ((i+1) == charRatios[0].length) { return "10%"; } else { return ""; }})
      .style("margin", "0px")
      .style("padding", "0px")
      .text(function(d,i) { if (i+1 == charRatios[0].length) { return d; } else { return charLookup[i]; }});
  }

  function evenOutLines() {



    var maxHeight = 0;
    $(".char-column").each(function() {
      var currHeight = $(this).height();
      if (currHeight > maxHeight) maxHeight = currHeight;
    });
    $(".char-column").each(function() {
      $(this).height(maxHeight);
    });
  }
