// Sequence Chart - SJG 01Dec2017

const char_A = "A";       // Adenine
const char_C = "C";       // Cytosine
const char_G = "G";       // Guanine
const char_R = "R";       // Purine
const char_T = "T";       // Thymine
const char_U = "U";       // Uracil
const char_Y = "Y";       // Pyrimidine
const char_space = "-";   // Space
const char_other = "*";   // Any other character

$(document).ready(function(){
  $('#fileInput').change(generateGraphic);
});

/* Generates and displays a sequence logo based on an input file */
function generateGraphic(e) {
  var file = e.target.files[0];                     // Pull in file
  if (file) {
    var r = new FileReader();
    r.onload = function(e) {
      var content = e.target.result;
      var lines = parseInputData(content);      // Parse file data
      if (lines.length < 1) {
        alert("The provided file does not contain valid sequence data" +
        "for visualization. Please try another file.");
      }
      var ratios = calcCharPercentages(lines);  // Calculate percentages
      drawChart(ratios);      // Draw figure
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
      for (k = 0; k < 7; k++) {                     // One letter per index
        finalArr[j].push(0);
      }
    }

    for (i = 0; i < numReads; i++) {             // Tally counts
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
          case case_space:
            finalArr[j][7]++;
            break;
          default:
            finalArr[j][8]++;
        }
      }
    }
    for (i = 0; i < numChars; i++) {             // Calculate ratios
      for (j = 0; j < 8; j++) {
        finalArr[i][j] = finalArr[i][j] / numChars;
      }
    }
    return finalArr;
  }

/* ACGRTUY-* */
  function drawChart(charRatios) {
    var charLookup = ["A", "C", "G", "R", "T", "U", "Y", "-", "*"];
    var colorLookup = ["green", "blue", "red", "purple", "orange", "cyan", "yellow", "black", "black"];
    d3.select("#sequenceGraphic")
    .selectAll("div")
    .data(charRatios)
      .enter()
      .append("div")
      .style("margin", "0px")
      .style("float", "left")
      .selectAll("p")
      .data( function(d,i,j) { return d; } )
      .enter()
      .append("p")
      .style("color", function(d,i) { return colorLookup[i]; })
      .style("font-size", function(d){ if (d > 0) { return d * 30 + "vh"; } else {return "0"; }})
      .style("margin", "0px")
      .style("padding", "0px")
      .text(function(d,i) { return charLookup[i]; });
  }


  /*function drawColumn(singleCharRatios) {
    var charLookup = ["A", "C", "G", "R", "T", "U", "Y", "-", "*"];
    var colorLookup = ["green", "blue", "red", "purple", "orange", "cyan", "yellow", "black", "black"];
    d3.select("#sequenceGraphic")
    .selectAll("div")
    .data(singleCharRatios)
      .enter()
      //.append("p") //removing
      //.selectAll("p") // these
      //.data( function(d,i,j) { return d; } ) //lines
      //.enter() //text displays normally
      .append("p")
      .style("color", function(d,i) { return colorLookup[i]; })
      .style("font-size", function(d){ if (d > 0) { return d * 30 + "vh"; } else {return "0"; }})
      .style("margin", "0px")
      .text(function(d,i) { return charLookup[i]; });
}*/

/* TEST CASES:
  - blank files, all file types, files with improper format
  - odd, even, zero number of sequences
  - sequences with differing sequence lengths
  - upper and lower case sequences
  - positions with 0, all, and in between values
  - test different browswers
*/