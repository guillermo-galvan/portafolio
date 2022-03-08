import { SubjectString } from './../../General/Observer/subjectString';
import { ObserverInnerHtml } from './../../General/Observer/observerInnerHtml';
import { BlogType } from './../../Types/BlogType';
import { SubjectBlogType } from './../../General/Observer/SubjectBlogType';
import { ObserverBlogType } from './../../General/Observer/ObserverBlogType';

const blogObserver = new SubjectBlogType();

function neweditblog(idurlText: string, idTitle: string, blogUrl: string, idBlog: string,
    idCreateDate: string, idImages: string, idDsc: string, idContent : string) {
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

    let blogInfo = <BlogType>JSON.parse(localStorage.getItem("blog"));
    let blogIds: BlogType = { id: idBlog, createDate: idCreateDate, images: idImages, title: idTitle, dsc: idDsc, content: idContent }

    if (blogInfo != undefined && blogInfo.content !== "") {
        let loadBackup = confirm("Hay informacion del blog, quiere utilizar estos datos para continuar ?")

        if (loadBackup) {
            (<HTMLInputElement>document.getElementById(blogIds.id)).value = blogInfo.id;
            (<HTMLInputElement>document.getElementById(blogIds.createDate)).value = blogInfo.createDate;
            (<HTMLInputElement>document.getElementById(blogIds.images)).value = blogInfo.images;
            (<HTMLInputElement>document.getElementById(blogIds.title)).value = blogInfo.title;
            (<HTMLInputElement>document.getElementById(blogIds.dsc)).value = blogInfo.dsc;
            (<HTMLTextAreaElement>document.getElementById(blogIds.content)).value = blogInfo.content;
        }
        else {
            localStorage.removeItem("blog");
        }
    }

    blogInfo = {
        id: (<HTMLInputElement>document.getElementById(blogIds.id)).value,
        createDate: (<HTMLInputElement>document.getElementById(blogIds.createDate)).value,
        images: (<HTMLInputElement>document.getElementById(blogIds.images)).value,
        title: (<HTMLInputElement>document.getElementById(blogIds.title)).value,
        dsc: (<HTMLInputElement>document.getElementById(blogIds.dsc)).value,
        content: (<HTMLTextAreaElement>document.getElementById(blogIds.content)).value,
    }

    let parent = new SubjectString();
    parent.subscribe(new ObserverInnerHtml(idurlText));
    
    blogObserver.subscribe(new ObserverBlogType(blogInfo));

    document.getElementById(blogIds.title).addEventListener("input", (event) => {
        let value = (<HTMLInputElement>event.target).value;
        parent.notify(`${blogUrl}/${value}`);
        blogObserver.notify({ id: null, createDate: null, images: null, title: value, dsc: null, content: null })
    });

    document.getElementById(blogIds.dsc).addEventListener("input", (event) => {
        let value = (<HTMLInputElement>event.target).value;        
        blogObserver.notify({ id: null, createDate: null, images: null, title: null, dsc: value, content: null })
    });
}

function changedContent(content: string) {
    blogObserver.notify({ id: null, createDate: null, images: null, title: null, dsc: null, content: content })
}

function changedImages(content: string) {
    blogObserver.notify({ id: null, createDate: null, images: content, title: null, dsc: null, content: null })
}

global.neweditblog = neweditblog;

global.changedContent = changedContent;

global.changedImages = changedImages;