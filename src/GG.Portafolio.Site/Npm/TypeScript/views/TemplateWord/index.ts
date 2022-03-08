import { SubjectString } from './../../General/Observer/subjectString';
import { ObserverInnerHtml } from './../../General/Observer/observerInnerHtml';

window.onload = function load() {
    var forms = document.querySelectorAll('.needs-validation');
    forms.forEach(x => {
        x.addEventListener('submit', (event) => {
            if (!(<HTMLFormElement>x).checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            x.classList.add('was-validated');
        }, false);
    });

    TemplateWord.Init();
};

class TemplateWord
{
    private readonly element: Element;
    private readonly rows: Element[];

    static Init = (): void => {
        new TemplateWord();
    };


    private constructor() {
        let column1Subject = new SubjectString();
        column1Subject.subscribe(new ObserverInnerHtml("HeaderColumn1"));

        document.getElementById("Column1").addEventListener("input", (event: InputEvent) => {
            column1Subject.notify((<HTMLInputElement>event.target).value);
        });

        let column2Subject = new SubjectString();
        column2Subject.subscribe(new ObserverInnerHtml("HeaderColumn2"));

        document.getElementById("Column2").addEventListener("input", (event) => {
            column2Subject.notify((<HTMLInputElement>event.target).value);
        });

        let column3Subject = new SubjectString();
        column3Subject.subscribe(new ObserverInnerHtml("HeaderColumn3"));

        document.getElementById("Column3").addEventListener("input", (event) => {
            column3Subject.notify((<HTMLInputElement>event.target).value);
        });

        document.getElementById("btnAdd").addEventListener("click", (event) => {
            this.Add();
        });

        document.getElementById("btnDelete").addEventListener("click", (event) => {
            this.Delete();
        });

        this.element = document.getElementById("detailRow");
        this.rows = [];
    }

    private createRow(): Element {
        let result = document.createElement("div");
        result.classList.add("row");
        return result;
    }

    private createDetailColumn(numberColummn: number): Element {
        let result = document.createElement("div");

        if (numberColummn == 1) {
            result.classList.add("col-md-3", "offset-md-1", "text-center");
        }
        else {
            result.classList.add("col-md-3", "text-center");
        }

        result.appendChild(this.createInput(numberColummn));

        return result;
    }

    private createInput(numberColummn: number): Element {
        let result = document.createElement("input");

        result.classList.add("form-control");

        result.setAttribute("type", "text");
        result.setAttribute("data-val", "true");
        result.setAttribute("data-val-required", "El campo es requerido.");
        result.setAttribute("id", `DetailRows_${this.rows.length}__DetailColumn${numberColummn}`);
        result.setAttribute("name", `DetailRows[${this.rows.length}].DetailColumn${numberColummn}`);
        result.setAttribute("placeholder", `Detalle columna ${numberColummn}`);
        result.setAttribute("value", "");
        result.setAttribute("required", "");
        result.setAttribute("aria-describedby", `DetailRows_${this.rows.length}__DetailColumn${numberColummn}-error`);
        result.setAttribute("aria-invalid", "false");

        return result;
    }


    private Add() : void {
        let row = this.createRow();

        for (var i = 1; i <= 3; i++) {
            row.appendChild(this.createDetailColumn(i));
        }
        
        this.rows.push(row);
        this.element.appendChild(row);
        this.element.appendChild(document.createElement("br"));
    }

    private Delete() : void {
        var row = this.rows.pop();
        this.element.removeChild(row);
    }

}