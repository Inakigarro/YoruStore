import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { AppButtonModule } from '@root/components/button/button.module';
import { CardUiComponent } from './card-ui/card-ui.component';

@NgModule({
    declarations: [CardUiComponent],
    imports: [
        NgOptimizedImage,
        CommonModule,
        MatCardModule,
        AppButtonModule
    ],
    exports: [CardUiComponent]
})
export class CardUiModule { }