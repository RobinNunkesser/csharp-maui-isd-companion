// Original Copyright (C) Thorsten Thormaehlen, Marburg, 2013, All rights reserved
// Contact: www.thormae.de
//
// Modified by Robin Nunkesser, 2025
// Changes: Adapted to work with Blazor 

// This software is written for educational (non-commercial) purpose. 
// There is no warranty or other guarantee of fitness for this software, 
// it is provided solely "as is".


export class NormalForm {
    myDiv;
    divId;
    cols;
    rows;
    data;
    normalType;
    that;
    language;
    scopeAttr;

    constructor(parentDivId, type, columns, lang) {
        this.divId = parentDivId;
        this.normalType = type;
        this.language = lang;
        this.cols = columns + 1;
        this.rows = Math.pow(2, columns);
        this.data = [];
        this.that = this;
        this.scopeAttr = Array.from(document.getElementById(parentDivId).attributes).find(attr => attr.name.startsWith('b-'));
        this.init();
    }

    init() {
        const parent = document.getElementById(this.divId);
        if (!parent) {
            console.log("NormalForm error: can not find an element with the given name: " + this.divId);
            return;
        }
        this.myDiv = document.createElement('div');
        this.myDiv.setAttribute(this.scopeAttr.name, '');
        if (!this.myDiv) {
            console.log("NormalFormerror: can not create a canvas element");
            return;
        }
        parent.appendChild(this.myDiv);
        for (let i = 0; i < this.rows; i++) {
            this.data[i] = 0;
        }
        this.update();
    }

    setColumns(columns) {
        var c = parseInt(columns);
        if (c < 1 && c > 6)
            return;
        this.cols = c + 1;
        this.rows = Math.pow(2, c);
        for (var i = 0; i < this.rows; i++) {
            this.data[i] = 0;
        }
        this.update();
    };

    genRandom() {
        for (var i = 0; i < this.rows; i++) {
            this.data[i] = Math.floor(Math.random() * 2);
        }
        this.update();
    };

    changeType(type) {
        this.normalType = parseInt(type);
        this.update();
    };

    update() {
        // clean up
        var oldTable = document.getElementById(this.divId + "_normalformtable");
        if (oldTable)
            this.myDiv.removeChild(oldTable);

        // re-generate
        var myTable = document.createElement('table');
        myTable.setAttribute('id', this.divId + "_normalformtable");
        myTable.setAttribute('class', 'normalformTableClass');
        myTable.setAttribute(this.scopeAttr.name, '');

        var myRow = document.createElement('tr');
        myRow.setAttribute(this.scopeAttr.name, '');
        for (var j = 0; j < this.cols; j++) {
            var myCell = document.createElement('th');
            myCell.setAttribute(this.scopeAttr.name, '');
            if (j < this.cols - 1) {
                myCell.innerHTML = "<i>x</i><sub><small>" + (this.cols - 2 - j) + "</small></sub>";
                myCell.setAttribute('class', 'normalformHeaderX normalformBit');
            } else {
                myCell.innerHTML = "<i>y</i>";
                myCell.setAttribute('class', 'normalformHeaderY normalformBit');
            }
            myRow.appendChild(myCell);
        }

        if (this.normalType >= 2) {
            var myCellDnfH = document.createElement('td');
            myCellDnfH.setAttribute(this.scopeAttr.name, '');
            myCellDnfH.innerHTML = "DNF";
            myCellDnfH.setAttribute('class', 'normalformHeaderRes normalformBit');
            myRow.appendChild(myCellDnfH);
        }
        if (this.normalType <= 2) {
            var myCellCnfH = document.createElement('td');
            myCellCnfH.setAttribute(this.scopeAttr.name, '');
            if (this.language === 0) {
                myCellCnfH.innerHTML = "CNF";
            } else {
                myCellCnfH.innerHTML = "KNF";
            }
            myCellCnfH.setAttribute('class', 'normalformHeaderRes normalformBit');
            myRow.appendChild(myCellCnfH);
        }
        myTable.appendChild(myRow);

        var firstDnfTerm = true;
        var firstCnfTerm = true;
        var emptyDnf = true;
        var emptyCnf = true;

        for (var i = 0; i < this.rows; i++) {
            myRow = document.createElement('tr');
            myRow.setAttribute(this.scopeAttr.name, '');
            var myCellDnf;
            var myCellCnf;
            if (this.normalType >= 2) {
                myCellDnf = document.createElement('td');
                myCellDnf.setAttribute(this.scopeAttr.name, '');
                myCellDnf.setAttribute('class', 'normalformBitRes');
            }
            var dnfStr = "";
            if (this.normalType <= 2) {
                var myCellCnf = document.createElement('td');
                myCellCnf.setAttribute(this.scopeAttr.name, '');
                myCellCnf.setAttribute('class', 'normalformBitRes');
            }
            var cnfStr = "";
            var res = i.toString(2);
            for (var j = 0; j < this.cols; j++) {
                var myCell = document.createElement('td');
                myCell.setAttribute(this.scopeAttr.name, '');

                if (j < this.cols - 1) { // x element
                    myCell.setAttribute('class', 'normalformBit');
                    var str;
                    if (j >= (this.cols - 1) - res.length) {
                        str = res.charAt(j - ((this.cols - 1) - res.length));
                        myCell.innerHTML = str;
                    } else {
                        str = "0";
                        myCell.innerHTML = str;
                    }

                    if (parseInt(str) === 1) {
                        if (dnfStr.length === 0) {
                            dnfStr += "(";
                            cnfStr += "(&not;";
                        } else {
                            dnfStr += "&and;";
                            cnfStr += "&or;&not;";
                        }
                    } else {
                        if (dnfStr.length === 0) {
                            dnfStr += "(&not;";
                            cnfStr += "(";
                        } else {
                            dnfStr += "&and;&not;";
                            cnfStr += "&or;";
                        }
                    }

                    dnfStr += "<i>x</i><sub><small>" + (this.cols - 2 - j) + "</small></sub>";
                    cnfStr += "<i>x</i><sub><small>" + (this.cols - 2 - j) + "</small></sub>";
                } else { // y element
                    myCell.setAttribute('class', 'normalformBit normalformBitY');
                    myCell.setAttribute('title', i);
                    myCell.onmousedown = (event) => {
                        this.myCellMouseDown(event);
                    };
                    myCell.ontouchend = (event) => {
                        this.myCellMouseDown(event);
                    }

                    if (this.data[i] === 0) {
                        myCell.innerHTML = "0";
                        dnfStr = "";
                        cnfStr += ")";
                        if (!firstCnfTerm) cnfStr = "&and;" + cnfStr;
                        firstCnfTerm = false;
                        emptyCnf = false;
                    } else {
                        myCell.innerHTML = "1";
                        dnfStr += ")";
                        if (!firstDnfTerm) dnfStr = "&or;" + dnfStr;
                        firstDnfTerm = false;
                        cnfStr = "";
                        emptyDnf = false;
                    }

                    if (i === (this.rows - 1)) {
                        if (emptyCnf) {
                            cnfStr += "&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;1&nbsp;&nbsp;";
                        }
                        if (emptyDnf) {
                            dnfStr += "&nbsp;&nbsp;&nbsp&nbsp;&nbsp&nbsp;0&nbsp;&nbsp;";
                        }
                    }
                }
                myRow.appendChild(myCell);
            }
            if (this.normalType >= 2) {
                myCellDnf.innerHTML = dnfStr;
                myRow.appendChild(myCellDnf);
            }
            if (this.normalType <= 2) {
                myCellCnf.innerHTML = cnfStr;
                myRow.appendChild(myCellCnf);
            }


            myTable.appendChild(myRow);
        }

        this.myDiv.appendChild(myTable);
    }

    myCellMouseDown(e) {
        var targ;
        if (e.target) {
            targ = e.target;
        } else { // deal with Microsoft
            if (e.srcElement)
                targ = e.srcElement;
        }
        if (targ.nodeType === 3) { // deal with Safari
            targ = targ.parentNode;
        }

        var i = parseInt(targ.title);

        if (i < 0 || i >= this.that.rows)
            return;

        if (this.that.data[i] === 0) {
            this.that.data[i] = 1;
        } else {
            this.that.data[i] = 0;
        }

        this.that.update();
    }
}


window.NormalForm = NormalForm;
