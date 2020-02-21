import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { ScrollToDirective } from './scroll-to.directive';
import { AfterIfDirective } from './after-if.directive';

@NgModule({
    declarations: [
        ScrollToDirective, AfterIfDirective
    ],
    imports: [
        CommonModule
    ],
    exports: [
        ScrollToDirective, AfterIfDirective
    ]
       
})
export class DirectivesModule { }
