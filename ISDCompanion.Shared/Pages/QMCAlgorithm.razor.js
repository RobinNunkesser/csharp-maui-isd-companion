// Original Copyright (C) Thorsten Thormaehlen, Marburg, 2012-2013, All rights reserved
// Contact: www.thormae.de
//
// Modified by Robin Nunkesser, 2025
// Changes: Adapted to work with Blazor 

// This software is written for educational (non-commercial) purpose. 
// There is no warranty or other guarantee of fitness for this software, 
// it is provided solely "as is". 

// Version 1.0, 2013
// Version 2.0, 2023 changed how the best solution is selected in Petrick's method  

function PetrickMethod() {
    this.problem;
    this.maxProblemSize = 100;
    this.solution;
    this.log = "";
    var that = this;

    this.test = function () {
        var andArray = new Array();
        var orArray;
        var monomA;
        var monomB;
        orArray = new Array();
        monomA = new Object(); // using objects ensures that (x and x) = x
        monomA[1] = 1;
        orArray.push(monomA);
        monomB = new Object();
        monomB[2] = 2;
        orArray.push(monomB);
        andArray.push(orArray);
        orArray = new Array();
        monomA = new Object();
        monomA[3] = 3;
        orArray.push(monomA);
        monomB = new Object();
        monomB[4] = 4;
        orArray.push(monomB);
        andArray.push(orArray);
        orArray = new Array();
        monomA = new Object();
        monomA[1] = 1;
        orArray.push(monomA);
        monomB = new Object();
        monomB[3] = 3;
        orArray.push(monomB);
        andArray.push(orArray);
        orArray = new Array();
        monomA = new Object();
        monomA[5] = 5;
        orArray.push(monomA);
        monomB = new Object();
        monomB[6] = 6;
        orArray.push(monomB);
        andArray.push(orArray);
        orArray = new Array();
        monomA = new Object();
        monomA[2] = 2;
        orArray.push(monomA);
        monomB = new Object();
        monomB[5] = 5;
        orArray.push(monomB);
        andArray.push(orArray);
        orArray = new Array();
        monomA = new Object();
        monomA[4] = 4;
        orArray.push(monomA);
        monomB = new Object();
        monomB[6] = 6;
        orArray.push(monomB);
        andArray.push(orArray);
        /*orArray = new Array();
         monomA = new Object(); 
         monomA[4] = 4;
         orArray.push(monomA);
         monomB = new Object();
         monomB[4] = 4;
         orArray.push(monomB);
         andArray.push(orArray);*/

        this.solve(andArray);
    };

    this.solve = function (eq) {

        this.problem = eq;
        this.log = "";

        //printEqnArray(eq);
        printEqnArrayFancy(eq);

        // multiply out
        var andArray = eq;
        var loopCounter = 0;
        while (andArray.length > 1) {
            var newAndArray = new Array();
            for (var i = 1; i < andArray.length; i += 2) {

                var orTermA = andArray[i - 1];
                var orTermB = andArray[i];
                var newOrArray = new Array();
                for (var a = 0; a < orTermA.length; a++) {
                    for (var b = 0; b < orTermB.length; b++) {
                        var monom1 = orTermA[a];
                        var monom2 = orTermB[b];
                        var resultingMonom = new Object();
                        for (var m in monom1) {
                            resultingMonom[monom1[m]] = monom1[m];
                        }
                        for (var n in monom2) {
                            resultingMonom[monom2[n]] = monom2[n];
                        }
                        newOrArray.push(resultingMonom);
                    }
                }

                newAndArray.push(newOrArray);
            }
            // if uneven copy last and-term
            if (andArray.length % 2 === 1) {
                newAndArray.push(andArray[andArray.length - 1]);
            }
            //printEqnArray(newAndArray);
            printEqnArrayFancy(newAndArray);

            andArray.length = 0;
            // simplify or-term
            for (var i = 0; i < newAndArray.length; i++) {
                var orTerm = newAndArray[i];
                var newOrTerm = simplifyOrTerm(orTerm);
                if (newOrTerm.length > 0) {
                    andArray.push(newOrTerm);
                }
            }

            var problemSize = eqnArrayProblemSize(andArray);
            if (problemSize > this.maxProblemSize) {
                console.log("Error: The cyclic covering problem is too large to be solved with Petrick's method (increase maxProblemSize). Size=" + problemSize);
                return false;
            }

            //printEqnArray(andArray);
            printEqnArrayFancy(andArray);
            loopCounter++;
        }
        this.solution = andArray;
        return true;
    };

    function simplifyOrTerm(orTerm) {
        // find a monom that is the same or simpler than another one
        var newOrTerm = new Array();
        var markedForDeletion = new Object();
        for (var a = 0; a < orTerm.length; a++) {
            var keepA = true;
            var monomA = orTerm[a];
            for (var b = a + 1; b < orTerm.length && keepA; b++) {
                var monomB = orTerm[b];
                var overlapBoverA = 0;
                var lengthA = 0;
                for (var m in monomA) {
                    if (monomB[m] in monomA) {
                        overlapBoverA++;
                    }
                    lengthA++;
                }

                var overlapAoverB = 0;
                var lengthB = 0;
                for (var m in monomB) {
                    if (monomA[m] in monomB) {
                        overlapAoverB++;
                    }
                    lengthB++;
                }

                if (overlapBoverA === lengthB) {
                    keepA = false;
                }

                if (lengthA < lengthB && overlapAoverB === lengthA) {
                    markedForDeletion[b] = b;
                }

            }
            if (keepA) {
                if (a in markedForDeletion) {
                    // do nothing
                } else
                    newOrTerm.push(orTerm[a]);
            }
        }
        return newOrTerm;
    }


    function printEqnArrayFancy(andArray) {
        var str = "";
        for (var i = 0; i < andArray.length; i++) {
            var first = true;
            str += "(";
            var orArray = andArray[i];
            for (var j = 0; j < orArray.length; j++) {
                if (!first)
                    str += " &or; ";
                var monom = orArray[j];
                for (var k in monom) {
                    str += "<i>p</i><sub><small>" + monom[k] + "</small></sub>";
                }
                first = false;
            }
            str += ")";
        }
        if (that.log.length > 0) {
            that.log += "<p>&hArr;&nbsp;" + str + "</p>";
        } else {
            that.log += "<p>" + str + "</p>";
        }
    }

    function eqnArrayProblemSize(andArray) {
        var monomCounter = 0;
        for (var i = 0; i < andArray.length; i++) {
            var orArray = andArray[i];
            monomCounter += orArray.length;
        }
        return monomCounter;
    }


    function printEqnArray(andArray) {
        var str = "";
        for (var i = 0; i < andArray.length; i++) {
            var first = true;
            str += "(";
            var orArray = andArray[i];
            for (var j = 0; j < orArray.length; j++) {
                if (!first)
                    str += " or ";
                var monom = orArray[j];
                for (var k in monom) {
                    str += monom[k];
                }
                first = false;
            }
            str += ")";
        }
        console.log(str);
    }

}

function PrimTerm() {
    this.implicant = -1;
    this.termString = "";
    this.color = [0, 0, 0];
    this.coloredTermString = "";
    this.used = false;
    this.neededByVar = new Object;
    this.varCount = 0;
}

function Implicant() {
    this.imp = new Object();
    this.isPrim = false;
    this.isOnlyDontCare = false;
    this.bitMask = 0;
}

function ImplicantGroup() {
    this.group = new Array;
    this.order = -1;
}

function PrimTermTable(ord) {
    this.essentialPrimTerms = new Array();
    this.order = ord;
    this.remainingVars = new Array();
    ;
    this.remainingPrimTerms = new Array();
    this.supersededPrimTerms = new Array();
}

function hsvToRgb(h, s, v) {

    var r, g, b;
    var i = Math.floor(h * 6);
    var f = h * 6 - i;
    var p = v * (1 - s);
    var q = v * (1 - f * s);
    var t = v * (1 - (1 - f) * s);

    switch (i % 6) {
        case 0:
            r = v, g = t, b = p;
            break;
        case 1:
            r = q, g = v, b = p;
            break;
        case 2:
            r = p, g = v, b = t;
            break;
        case 3:
            r = p, g = q, b = v;
            break;
        case 4:
            r = t, g = p, b = v;
            break;
        case 5:
            r = v, g = p, b = q;
            break;
    }

    return [Math.floor(r * 255), Math.floor(g * 255), Math.floor(b * 255)];
}

class QuineMcCluskeyDataCtrl {
    noOfVars;
    funcdata;
    primTerms;
    implicantGroups;
    minimalTerm;
    coloredMinimalTerm;
    minimalTermPrims;
    primTermTables;
    petrickSolver;
    petrickTermPrims;
    allowDontCare;

    constructor() {
        this.noOfVars = -1;
        this.funcdata = new Array;
        this.primTerms = new Array;
        this.implicantGroups = new Array;
        this.minimalTerm = "";
        this.coloredMinimalTerm = "";
        this.minimalTermPrims = new Array;
        this.primTermTables = new Array;
        this.petrickSolver = new PetrickMethod();
        this.petrickTermPrims = new Array;
        this.allowDontCare = false;
    }

    init(no) {
        this.noOfVars = no;
        this.funcdata.length = 0;
        this.primTerms.length = 0;
        this.implicantGroups.length = 0;
        this.minimalTerm = "0";
        this.coloredMinimalTerm = "0";
        this.minimalTermPrims.length = 0;
        this.primTermTables.length = 0;
        this.petrickTermPrims.length = 0;

        var noOfFuncData = Math.pow(2, this.noOfVars);
        for (var i = 0; i < noOfFuncData; i++) {
            this.funcdata.push(0);
        }
    }
}

export class QuineMcCluskey {
    myDiv;
    divId;
    cols;
    rows;
    data;
    that;
    language;
    scopeAttr;
    labels;

    constructor(parentDivId, columns, language) {
        this.myDiv = -1;
        this.divId = parentDivId;
        this.cols = columns + 1;
        this.rows = Math.pow(2, columns);
        this.data = new QuineMcCluskeyDataCtrl();
        this.that = this;
        this.scopeAttr = Array.from(document.getElementById(parentDivId).attributes).find(attr => attr.name.startsWith('b-'));

        if (language === 0) {
            this.labels = {
                ttable: "Truth table",
                minExp: "Minimal boolean expression",
                impli: "Implicants",
                order: "Order",
                primChart: "Prime implicant chart",
                primChartReduced: "Reduced prime implicant chart (Iteration",
                extractedPrims: "Extracted essential prime implicants",
                extractedMPrims: "Extracted prime implicants",
                petricksM: "Petrick's method"
            };
        } else {
            this.labels = {
                ttable: "Wahrheitstafel",
                minExp: "Minimaler boolescher Ausdruck",
                impli: "Implikanten",
                order: "Ordnung",
                primChart: "Primimplikantentafel",
                primChartReduced: "Reduzierte Primimplikantentafel (Iteration",
                extractedPrims: "Extrahierte essentielle Primimplikanten",
                extractedMPrims: "Extrahierte Primimplikanten",
                petricksM: "Verfahren von Petrick"
            };

        }
        this.init();
    }

    init() {
        this.data.init(this.cols - 1);

        this.myDiv = document.createElement('div');
        if (!this.myDiv) {
            console.log("QuineMcCluskey error: can not create a canvas element");
            this.myDiv = -1;
        } else {

            var parent = document.getElementById(this.divId);
            if (!parent) {
                if (this.divId !== "fakeDivId") {
                    console.log("QuineMcCluskey error: can not find an element with the given name: " + this.divId);
                }
                this.myDiv = -1;
            } else {
                document.body.appendChild(this.myDiv);
                parent.appendChild(this.myDiv);
            }
        }
        this.update();
    }

    update() {
        if (this.myDiv === -1) return;

        // clean up
        var oldInnerDiv = document.getElementById(this.divId + "_innerDiv");
        if (oldInnerDiv) this.myDiv.removeChild(oldInnerDiv);

        var myInnerDiv = document.createElement('div');
        myInnerDiv.setAttribute('id', this.divId + "_innerDiv");


        var myTruthTableDiv = document.createElement('div');
        myTruthTableDiv.innerHTML = "<div>" + this.labels['ttable'] + ":</div>";
        myTruthTableDiv.setAttribute('class', 'qmcTableLabelDiv');

        // re-generate
        var myTable = document.createElement('table');
        myTable.setAttribute('class', 'qmcTableClass');

        var myRow = document.createElement('tr');

        var cell1h = document.createElement('td');
        cell1h.innerHTML = "";
        cell1h.setAttribute('class', 'qmcTdNoBorder');
        myRow.appendChild(cell1h);

        for (var j = 0; j < this.cols; j++) {
            var myCell = document.createElement('th');
            if (j < this.cols - 1) {
                myCell.innerHTML = "<i>x</i><sub><small>" + (this.cols - 2 - j) + "</small></sub>";
                myCell.setAttribute('class', 'qmcHeaderX qmcBit');
            } else {
                myCell.innerHTML = "<i>y</i>";
                myCell.setAttribute('class', 'qmcHeaderY qmcBit');
            }
            myRow.appendChild(myCell);
        }
        myTable.appendChild(myRow);


        for (var i = 0; i < this.rows; i++) {
            myRow = document.createElement('tr');

            var cell1 = document.createElement('td');
            cell1.innerHTML = i.toString(10) + ":";
            cell1.setAttribute('class', 'qmcTdNoBorder');
            myRow.appendChild(cell1);

            var res = i.toString(2);
            for (var j = 0; j < this.cols; j++) {
                var myCell = document.createElement('td');

                if (j < this.cols - 1) { // x element
                    myCell.setAttribute('class', 'qmcBit');
                    var str;
                    if (j >= (this.cols - 1) - res.length) {
                        str = res.charAt(j - ((this.cols - 1) - res.length));
                        myCell.innerHTML = str;
                    } else {
                        str = "0";
                        myCell.innerHTML = str;
                    }
                } else { // y element
                    myCell.setAttribute('class', 'qmcBit qmcBitY');
                    myCell.setAttribute('title', i);
                    myCell.onmousedown = function (event) {
                        this.myCellMouseDown(event);
                    };

                    if (this.data.funcdata[i] === 0) {
                        myCell.innerHTML = "0";
                    }
                    if (this.data.funcdata[i] === 1) {
                        myCell.innerHTML = "1";
                    }
                    if (this.data.funcdata[i] === 2) {
                        myCell.innerHTML = "&times;";
                    }
                }
                myRow.appendChild(myCell);
            }
            myTable.appendChild(myRow);
        }
    }
}

window.QuineMcCluskey = QuineMcCluskey;