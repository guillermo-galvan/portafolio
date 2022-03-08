export class TxtRotate {

    #element: Element;
    #textsRotate: string[];
    #period: number;
    #loopNum: number = 0;
    #text: string = '';
    #isDeleting: boolean = false;
    #continueTxtRotate: boolean = true;

    constructor(element: Element, texts: string[], period?: number) {
        this.#element = element;
        this.#textsRotate = texts;
        this.#period = period || 2000;        
    }

   set IsDeleting(value: boolean) {
        this.#isDeleting = value;
    }

    get ContinueTxtRotate() : boolean {
        return this.#continueTxtRotate;
    }

    set ContinueTxtRotate(value: boolean) {
        this.#continueTxtRotate = value;
    }

    private nexTo(this: TxtRotate) : number {
        let i : number = this.#loopNum % this.#textsRotate.length;
        let fullTxt :string = this.#textsRotate[i];

        if (this.#isDeleting) {
            this.#text = fullTxt.substring(0, this.#text.length - 1);
        } else {
            this.#text = fullTxt.substring(0, this.#text.length + 1);
        }

        this.#element.innerHTML = '<span class="wrap">' + this.#text + '</span>';
        let delta = 200 - Math.random() * 100;

        if (this.#isDeleting) {
            delta /= 2;
        }

        if (!this.#isDeleting && this.#text === fullTxt) {
            delta = this.#period;
            this.#isDeleting = true;
        } else if (this.#isDeleting && this.#text === '') {
            this.#isDeleting = false;
            this.#loopNum++;
            delta = 100;
        }

        return delta;
    }

    public validateTime(txtRotate:TxtRotate,e?:any) {
        let delay = txtRotate.nexTo();

        if (txtRotate.ContinueTxtRotate) {
            setTimeout(txtRotate.validateTime.bind(event, txtRotate), delay);
        }
    }
}