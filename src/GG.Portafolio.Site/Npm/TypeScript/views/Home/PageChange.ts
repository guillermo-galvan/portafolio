import { AnimateTransition } from './../../General/animateTransitionEvent';
import { AnimateTransitionType } from './../../Types/AnimateTransitionType';
import { PageChangeAnimateCss } from './../../Types/PageChangeAnimateCss';
import { ILoadPage } from './../../Interface/iLoadPage';
import { Home } from './home';
import { AboutMe } from './aboutMe';
import { Blog } from './blog';
import { Portfolio } from './portfolio';
import { AnimateTransitionEventType } from './../../General/Enum/AnimateTransitionEventType';

export class PageChange {
    #cssStartAnimation: PageChangeAnimateCss = "begin-transition-animate";
    #cssEndAnimation: PageChangeAnimateCss = "end-transition-animate";
    #cssLeft: PageChangeAnimateCss = "transition-left-lessthan-100";
    #cssMiddle: PageChangeAnimateCss = "transition-left-0";
    #cssRigth: PageChangeAnimateCss = "transition-left-greaterthan-100";    
    #items: ILoadPage[] = [];
    #idSelectItem: ILoadPage | null;

    static Init = (index : number): PageChange => {
        let pages: ILoadPage[] = [];

        pages.push(new Home());
        pages.push(new AboutMe());
        pages.push(new Blog());
        pages.push(new Portfolio());        

        return new PageChange(index, pages);
    };

    private animateTransition: AnimateTransition<PageChange>;

    private constructor(index: number, pages: ILoadPage[]) {        
        this.#items = pages;
        this.Init(index);
    }

    private Init(index: number) {
        let setting: AnimateTransitionType<PageChange> = { IdElement: "divTransition", Events: [] };
        setting.Events.push({ EventType: AnimateTransitionEventType.animationend, Event: this.end, ParamEvent: this });        
        this.animateTransition = new AnimateTransition(setting);

        this.#items.forEach(item => {
            item.menu.addEventListener("click", this.onEvent.bind(event, this, item));
        });
        this.#idSelectItem = this.#items[index];
        this.#idSelectItem.isLoaded = true;
        this.#idSelectItem.load();
        this.setDisplay(this);
    }

    private changeDisabled(pagechange: PageChange) {
        pagechange.#items.forEach(x => x.menu.classList.toggle("disabled"));
    }

    private changeScrollHidden(pagechange: PageChange) {
        document.querySelector("body").classList.toggle("scroll-hidden");
    }

    private setHidden(pagechange: PageChange) {
        if (pagechange.#idSelectItem) {
            pagechange.#items.forEach(x => {                
                x.page.classList.add("display-none");
            });
        }
    }

    private setDisplay(pagechange: PageChange) {        
        pagechange.#idSelectItem.page.classList.remove("display-none");
        pagechange.#idSelectItem = null;
    }

    end(pagechange: PageChange, e: AnimationEvent): void {
        pagechange.setHidden(pagechange);
        if (pagechange.animateTransition.Element.classList.contains(pagechange.#cssStartAnimation)) {
            pagechange.animateTransition.Element.classList.add(pagechange.#cssMiddle);
            pagechange.animateTransition.Element.classList.remove(pagechange.#cssStartAnimation, pagechange.#cssLeft);
            let isLoading = true;

            do {
                if (pagechange.#idSelectItem.isLoaded) {
                    isLoading = false;
                }
            } while (isLoading);

            pagechange.setDisplay(pagechange);
            pagechange.animateTransition.Element.classList.add(pagechange.#cssEndAnimation);
        }
        else if (pagechange.animateTransition.Element.classList.contains(pagechange.#cssEndAnimation)) {            
            pagechange.animateTransition.Element.classList.add(pagechange.#cssRigth);
            pagechange.animateTransition.Element.classList.remove(pagechange.#cssEndAnimation, pagechange.#cssMiddle);
            pagechange.animateTransition.Element.classList.add(pagechange.#cssLeft);
            pagechange.animateTransition.Element.classList.remove(pagechange.#cssRigth);            
            pagechange.changeDisabled(pagechange);
            pagechange.changeScrollHidden(pagechange);            
        }
    }

    onEvent(pagechange: PageChange, page: ILoadPage, event: PointerEvent) {
        window.scrollTo({ top: 0, left: 0, behavior: 'smooth' });
        window.scrollBy({ top: 0, left: 0, behavior: 'smooth' });
        
        let toogle = document.getElementById("btnNvarToggle");

        if (toogle) {
            toogle?.blur();
            toogle.setAttribute("aria-expanded", "false");
            toogle.classList.add("collapsed");
            let idChildren = toogle.getAttribute("aria-controls");
            toogle = document.getElementById(idChildren);
            toogle?.classList.remove("show");
        }
        pagechange.#idSelectItem = page;
        pagechange.changeDisabled(pagechange);
        pagechange.animateTransition.Element.classList.toggle(pagechange.#cssStartAnimation);
        pagechange.changeScrollHidden(pagechange);
        pagechange.#items.forEach(x => x.unLoad());
        page.load();
    }
}