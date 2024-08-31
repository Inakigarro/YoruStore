import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { AppButtonModule } from '@root/components/button/button.module';
import { CardUiComponent } from './card-ui.component';

@NgModule({
    declarations: [CardUiComponent],
    imports: [
        CommonModule,
        MatCardModule,
        AppButtonModule
    ],
    exports: [CardUiComponent]
})
export class CardUiModule { }