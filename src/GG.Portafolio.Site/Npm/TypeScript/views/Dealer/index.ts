import { ElementRow } from './../../Types/ElementRow';
import { DeliveryMan } from './../../Types/DeliveryMan';
import { TimeCalculated } from './../../Types/TimeCalculated';
import { ResponseDealer } from './../../Types/ResponseDealer';

let dealerClass: Dealer;

window.onload = function load() {
    dealerClass = Dealer.Init();
    dealerClass.start();
};

class Dealer {

    private date: Date;
    private urlData: string;    
    private readonly _elements: ElementRow[];
    private _deliveries: DeliveryMan[];
    private readonly mainElement: Element;

    static Init(): Dealer {
        return new Dealer();
    }

    get Elements(){
        return this._elements
    }

    get Deliveries() {
        return this._deliveries;
    }

    private constructor() {
        this.date = new Date(localStorage.getItem('Date'));
        this.urlData = (<HTMLInputElement>document.getElementById("urlData")).value;
        this._elements = [];
        this.mainElement = document.getElementById("divDetail");
    }

    public start() {        
        let date = new Date(Date.now());

        if (this.date === undefined || this.date === null || date > this.date || isNaN(this.date.getTime()) ||
            (date.toLocaleDateString() === this.date.toLocaleDateString() && date.toLocaleTimeString() > this.date.toLocaleTimeString())) {
            this.dataLoad(date);
        }
        else {            
            this._deliveries = <DeliveryMan[]>JSON.parse(localStorage.getItem("Deliveries"));            
            let timeCalculateds: TimeCalculated[] = [];
            let option = 1;

            while (option < 24) {
                let data = <TimeCalculated>JSON.parse(localStorage.getItem(`Option${option++}`));
                let dateData = new Date(data.time);

                if (dateData <= date) {
                    data.status = 0;
                }
                timeCalculateds.push(data);
            }

            this.dataFill(timeCalculateds);
        }
    }

    public getSelect(data: TimeCalculated): HTMLSelectElement {
        let result = document.createElement("select");

        result.classList.add("form-select");

        let option = document.createElement("option");
        option.value = "";
        option.text = "Seleccione...";
        result.options.add(option);

        let date = new Date(data.time);
        let dateinicial = new Date(date.getTime() - (30 * 60000));
        let datefinal = new Date(date.getTime() + (30 * 60000));

        this._deliveries.forEach(deliveryman => {
            var exist = false;

            deliveryman.dates.forEach(function (x) {
                const y = new Date(x);

                if (!exist)
                    exist = dateinicial <= y && y <= datefinal;
            });

            option = document.createElement("option");
            option.value = deliveryman.id.toString();
            option.text = deliveryman.name;

            if (exist) {
                option.disabled = true;
            }

            result.options.add(option);
        });


        return result;
    }

    public getSpan(innerHtml: string): HTMLSpanElement {
        let span = document.createElement("span");
        span.classList.add("align-middle");
        span.innerHTML = innerHtml;
        return span;
    }

    private async dataLoad(date: Date) {        
        await fetch(this.urlData + `?date=${date.getMonth() + 1}%2F${date.getDate()}%2F${date.getFullYear()}%20${date.getHours()}%3A${date.getMinutes()}%3A${date.getSeconds()}`)
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                else {
                    return { success : false};
                }
            })
            .then(json => { return <ResponseDealer>json; })
            .then(result => {
                if (result.success) {
                    localStorage.setItem("Deliveries", JSON.stringify(result.deliveries));
                    localStorage.setItem("Date", result.dates.baseDay.toString());
                    this._deliveries = result.deliveries;
                    this.dataFill(result.dates.timeCalculateds);
                }
            })
            .catch(error => {
                console.log(error);
            });
    }

    private dataFill(timeCalculateds: TimeCalculated[]) {

        timeCalculateds.forEach(data => {
            localStorage.setItem(`Option${data.id}`, JSON.stringify(data));
            let className: string = null;
            let colClassName: string = null;
            let date = new Date(data.time);

            switch (data.status) {
                case 1:
                    className = "status-active";
                    colClassName = "col-md-10";
                    break;

                case 2:
                    className = "status-selected";
                    colClassName = "col-md-5";
                    break;

                default:
                    className = "status-disabled";
                    colClassName = "col-md-10";
                    break;
            }

            let row = this.getDiv('row', 'g-0');
            row.setAttribute("onclick", `selectedItem(${data.id})`);

            let firstColumn = this.getDiv("col", colClassName, "offset-md-1", className, "text-center");            
            firstColumn.appendChild(this.getSpan(date.toLocaleDateString() + ' ' + date.toLocaleTimeString()));

            let secondColumn = this.getDiv("col", "col-md-5", className, "text-center", (data.status == 2 ? "" : "d-none"));
            if (data.distributor > 0) {
                let distributor = this._deliveries.find(x => x.id == data.distributor);
                secondColumn.appendChild(this.getSpan(distributor.name));
                row.setAttribute("onclick", `detelestate(${data.id})`);
            }

            let thirdColum = this.getDiv("col-md-3", "offset-md-1", "status-active", "text-center", "d-none");
            thirdColum.appendChild(this.getSpan(date.toLocaleDateString() + ' ' + date.toLocaleTimeString()));

            let fourthColumn = this.getDiv("col-md-3", "status-active", "text-center", "d-none");

            let fivethColumn = this.getDiv("col-md-4", "status-active", "text-center", "d-none");
            fivethColumn.appendChild(this.getButton(data.id));

            row.appendChild(firstColumn);
            row.appendChild(secondColumn);
            row.appendChild(thirdColum);
            row.appendChild(fourthColumn);
            row.appendChild(fivethColumn);

            this._elements.push({
                row: row,
                timeCalculated: data,
                firstColumn: firstColumn,
                secondColumn: secondColumn,
                thirdColumn: thirdColum,
                fourthColumn: fourthColumn,
                fivethColumn: fivethColumn
            });

            this.mainElement.appendChild(row);
            this.mainElement.appendChild(document.createElement("br"));
        });
    }

    private getDiv(...className: string[]): HTMLDivElement {
        let div = document.createElement("div");
        className.filter(x => x !== "").forEach(x => div.classList.add(x));
        return div;
    }

    private getButton(id: number) : HTMLButtonElement {
        let button = document.createElement("button");
        button.setAttribute("type", "button");
        button.setAttribute("data-id", id.toString());
        button.classList.add("btn", "btn-outline-primary");
        button.innerHTML = "Guardar";
        button.setAttribute("onclick", `savestate(event,${id});`);
        return button;
    }
}

function selectedItem(id: number) {
    dealerClass.Elements.filter(x => x.timeCalculated.status !== 0).forEach(x => {

        if (x.timeCalculated.id === id && x.timeCalculated.status === 1) {
            x.row.removeAttribute("onclick");
            x.row.classList.add("row-select");
            x.firstColumn.classList.add("d-none");
            x.secondColumn.classList.add("d-none");
            x.thirdColumn.classList.remove("d-none");
            x.fourthColumn.classList.remove("d-none");
            x.fivethColumn.classList.remove("d-none");
            x.fourthColumn.innerHTML = "";
            x.fourthColumn.appendChild(dealerClass.getSelect(x.timeCalculated));
        }
        else if (x.timeCalculated.status === 1) {
            x.row.setAttribute("onclick", `selectedItem(${x.timeCalculated.id})`);
            x.row.classList.remove("row-select");
            x.firstColumn.classList.remove("d-none");

            if (x.timeCalculated.distributor == 0) {
                x.secondColumn.classList.add("d-none");
                x.firstColumn.classList.remove("col-md-5","status-selected");
                x.firstColumn.classList.add("col-md-10","status-active");
            }
            else {
                x.firstColumn.classList.add("col-md-5");
                x.firstColumn.classList.remove("d-none");
                x.secondColumn.classList.remove("d-none");
            }
            
            x.thirdColumn.classList.add("d-none");
            x.fourthColumn.classList.add("d-none");
            x.fivethColumn.classList.add("d-none");
        }
    });
}

function savestate(event: PointerEvent,id: number) {

    let dealer = dealerClass.Elements.find(x => x.timeCalculated.id == id);

    let select = <HTMLSelectElement>dealer.fourthColumn.firstElementChild;
    let selectedOption = select.options[select.selectedIndex];

    if (selectedOption.value === "") {
        return false;
    }

    dealer.firstColumn.classList.remove("status-active", "d-none","col-md-10");
    dealer.secondColumn.classList.remove("status-active", "d-none");

    dealer.firstColumn.classList.add("status-selected", "col-md-5");
    dealer.secondColumn.classList.add("status-selected", "col-md-5");

    dealer.thirdColumn.classList.add("d-none");
    dealer.fourthColumn.classList.add("d-none");
    dealer.fivethColumn.classList.add("d-none");

    let idDeliveriesMan = parseInt(selectedOption.value);
    dealer.timeCalculated.distributor = idDeliveriesMan;
    dealer.timeCalculated.status = 2;

    let deliverMan = dealerClass.Deliveries.find(x => x.id == idDeliveriesMan);
    dealer.secondColumn.innerHTML = "";
    dealer.secondColumn.appendChild(dealerClass.getSpan(deliverMan.name));
    localStorage.setItem(`Option${dealer.timeCalculated.id}`, JSON.stringify(dealer.timeCalculated));

    
    deliverMan.dates.push(dealer.timeCalculated.time);
    localStorage.setItem("Deliveries", JSON.stringify(dealerClass.Deliveries));
    dealer.row.setAttribute("onclick", `detelestate(${dealer.timeCalculated.id})`);
    dealer.row.classList.remove("row-select");
    event.stopPropagation();
}

function detelestate(id: number) {
    
    let dealer = dealerClass.Elements.find(x => x.timeCalculated.id == id);

    let iddeliveriesman = dealer.timeCalculated.distributor;
    dealer.timeCalculated.distributor = 0;
    dealer.timeCalculated.status = 1;
    localStorage.setItem(`Option${dealer.timeCalculated.id}`, JSON.stringify(dealer.timeCalculated));

    let deliverMan = dealerClass.Deliveries.find(x => x.id == iddeliveriesman);
    deliverMan.dates = deliverMan.dates.filter(x => x !== dealer.timeCalculated.time);
    localStorage.setItem("Deliveries", JSON.stringify(dealerClass.Deliveries));

    dealer.row.removeAttribute("onclick");
    selectedItem(id);
}

global.selectedItem = selectedItem;
global.savestate = savestate;
global.detelestate = detelestate;
