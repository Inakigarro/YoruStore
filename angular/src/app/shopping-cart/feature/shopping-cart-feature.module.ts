import { NgModule } from "@angular/core";
import { ShoppingCartFeatureComponent } from "./shopping-cart-feature.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { CardFeatureModule } from "@root/components/card/feature/card-feature.module";
import { ShoppingCartUiModule } from "../ui/shopping-cart-ui.module";
import { AppButtonModule } from "@root/components";

@NgModule({
	declarations: [ShoppingCartFeatureComponent],
	imports: [
		CommonModule,
		MatCardModule,
		CardFeatureModule,
		ShoppingCartUiModule,
		AppButtonModule,
	],
	exports: [ShoppingCartFeatureComponent],
})
export class ShoppingCartFeatureModule {}
