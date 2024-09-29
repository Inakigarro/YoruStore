import { NgModule } from "@angular/core";
import { ShoppingCartUiComponent } from "./shopping-cart-ui.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { MatListModule } from "@angular/material/list";
import { AppCardModule } from "@root/components/card/card.module";
import { MatDividerModule } from "@angular/material/divider";

@NgModule({
	declarations: [ShoppingCartUiComponent],
	imports: [CommonModule, AppCardModule, MatDividerModule],
	exports: [ShoppingCartUiComponent],
})
export class ShoppingCartUiModule {}
