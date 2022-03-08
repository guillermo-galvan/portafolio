import { changeCaracter } from './../../General/functions';

window.onload = function load() {
    var forms = document.querySelectorAll('.needs-validation');

    const divComments: HTMLDivElement = <HTMLDivElement>document.getElementById("divComments");

    forms.forEach(x => {
        x.addEventListener('submit', (event) => {
            x.classList.add('was-validated');

            if ((<HTMLFormElement>x).checkValidity()) {
                let data = new FormData((<HTMLFormElement>x));
                let url = x.getAttribute("action");

                fetch(url, { body: data, method: "post" })
                    .then(response => response.json())
                    .then(json => {
                        if (json.success) {
                            let row = document.createElement("div");
                            row.classList.add("row");

                            let column = document.createElement("div");
                            column.classList.add("col-md-10", "offset-1");

                            let card = document.createElement("div");
                            card.classList.add("card", "border-dark", "mb-3");

                            let cardHeader = document.createElement("div");
                            cardHeader.classList.add("card-header", "bg-transparent", "border-dark");
                            cardHeader.innerHTML = `${json.name} - ${json.date}`;

                            let cardBody = document.createElement("div");
                            cardBody.classList.add("card-body","text-dark");

                            let paragraph = document.createElement("p");
                            paragraph.classList.add("card-text");
                            paragraph.innerHTML = changeCaracter(json.comment);

                            cardBody.appendChild(paragraph);

                            card.appendChild(cardHeader);
                            card.appendChild(cardBody);

                            column.appendChild(card);
                            row.appendChild(column);

                            divComments.appendChild(row);
                            (<HTMLFormElement>x).reset();
                        }
                    })
                    .catch(error => console.error('Error:', error));
            }


            event.preventDefault();
            event.stopPropagation();
            return false;
            
        }, false);
    });

};