import { NgModule } from "@angular/core";
import { ShoppingCartFeatureComponent } from "./shopping-cart-feature.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { CardFeatureModule } from "@root/components/card/feature/card-feature.module";

@NgModule({
	declarations: [ShoppingCartFeatureComponent],
	imports: [CommonModule, MatCardModule, CardFeatureModule],
	exports: [ShoppingCartFeatureComponent],
})
export class ShoppingCartFeatureModule {}
