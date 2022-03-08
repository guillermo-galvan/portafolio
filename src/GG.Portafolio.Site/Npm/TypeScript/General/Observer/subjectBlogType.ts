import { INotify } from './../../Interface/iNotify';
import { Observer } from './../../General/Observer/observer';
import { BlogType } from './../../Types/BlogType';

export class SubjectBlogType extends Observer<BlogType> implements INotify<BlogType>{
    constructor() {
        super();
    }

    notify(model: BlogType): void {
        super.notify(model);
    }
}