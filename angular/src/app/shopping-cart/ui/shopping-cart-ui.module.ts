import { NgModule } from "@angular/core";
import { ShoppingCartUiComponent } from "./shopping-cart-ui.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { MatListModule } from "@angular/material/list";

@NgModule({
    declarations: [ShoppingCartUiComponent],
    imports: [
        CommonModule,
        MatCardModule,
        MatListModule
    ],
    exports: [ShoppingCartUiComponent],
})
export class ShoppingCartUiModule {}