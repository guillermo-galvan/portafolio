import { INotify } from './../../Interface/iNotify';
import { BlogType } from './../../Types/BlogType';

export class ObserverBlogType implements INotify<BlogType> {

    private blogInfo: BlogType;

    constructor(blogInfo: BlogType) {

        this.blogInfo = blogInfo;
    }

    notify(model: BlogType) {
        if (model.title !== null && model.title !== "") {
            this.blogInfo.title = model.title;
        }

        if (model.content !== null && model.content !== "") {
            this.blogInfo.content = model.content;
        }

        if (model.images !== null && model.images !== "") {
            this.blogInfo.images = model.images;
        }

        if (model.dsc !== null && model.dsc !== "") {
            this.blogInfo.dsc = model.dsc;
        }

        localStorage.setItem("blog", JSON.stringify(this.blogInfo));
    }
}