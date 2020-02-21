import { Directive, ElementRef, Inject, Input } from '@angular/core';

@Directive({
    selector: '[scrollTo]'
})
export class ScrollToDirective {
    @Input('scrollTo') focus: boolean;
    @Input('scrollContainerClass') scrollContainerClass: string;
    constructor(@Inject(ElementRef) private element: ElementRef) { }
    protected ngOnChanges() {
        if (this.focus) {
            let elem = document.getElementsByClassName(this.scrollContainerClass);

            for (let i = elem.length > 1? 1 : 0; i < elem.length; i++) {
                elem[i].scrollTo({ top: this.element.nativeElement.offsetTop - this.element.nativeElement.offsetTop * 0.1 });
            }
        }
    }
}
