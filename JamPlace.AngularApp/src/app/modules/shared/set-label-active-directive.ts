import { Directive, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { FormControlName } from '@angular/forms';
import { Subscription } from 'rxjs';



@Directive({
    selector: '[setLabelActive]'
})
export class SetLabelActiveDirective implements OnDestroy {

    valueSub: Subscription; 

    constructor(
        private el: ElementRef,
        private formControlName: FormControlName // Inject FormControlName
    ) {

    }

    ngOnInit() {
        // Listen value changes
        this.valueSub = this.formControlName.valueChanges.subscribe(value => {

            // Get label
            const inputId = this.el.nativeElement.getAttribute('id'),
                label = document.querySelector(`label[for="${inputId}"]`);

            // Toggle `active` class
            if (label) {
                label.classList.toggle('active', value);
            }
        });
    }

    ngOnDestroy() {
        // Unlisten value changes
        this.valueSub.unsubscribe();
    }

}