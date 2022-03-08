import { AnimateTransitionType } from './../Types/AnimateTransitionType';
import { AnimateTransitionEventType } from './../General/Enum/AnimateTransitionEventType';

export class AnimateTransition<T> {

    private setting: AnimateTransitionType<T>;
    private element: Element;

    constructor(setting: AnimateTransitionType<T>) {
        this.setting = setting;
        this.element = document.getElementById(this.setting.IdElement);
        this.init();
    }

    public get Element() {
        return this.element;
    }

    public set Element(elementd: Element) {
        this.element = elementd;
    }

    private init(this: AnimateTransition<T>) {

        this.Element = document.getElementById(this.setting.IdElement);

        if (!this.element) {
            throw new Error("Element was not found");
        }

        if (this.setting.Events) {

            this.setting.Events.filter(x => x.EventType == AnimateTransitionEventType.animationcancel).forEach(x => {
                this.element.addEventListener("animationcancel", x.Event.bind(event,x.ParamEvent), false);
            });

            this.setting.Events.filter(x => x.EventType == AnimateTransitionEventType.animationstart).forEach(x => {
                this.element.addEventListener("animationstart", x.Event.bind(event, x.ParamEvent), false);
            });

            this.setting.Events.filter(x => x.EventType == AnimateTransitionEventType.animationiteration).forEach(x => {
                this.element.addEventListener("animationiteration", x.Event.bind(event, x.ParamEvent), false);
            });

            this.setting.Events.filter(x => x.EventType == AnimateTransitionEventType.animationend).forEach(x => {
                this.element.addEventListener("animationend", x.Event.bind(event, x.ParamEvent), false);
            });

        }
    }
}