import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import {
	SHOPPING_CART_FEATURE_KEY,
	shoppingCartReducer,
} from "./state/shopping-cart.reducer";
import { ShoppingCartFeatureModule } from "./feature/shopping-cart-feature.module";
import { EffectsModule } from "@ngrx/effects";
import { ShoppingCartEffects } from "./state/shopping-cart.effects";

@NgModule({
	declarations: [],
	imports: [
		ShoppingCartFeatureModule,
		StoreModule.forFeature(SHOPPING_CART_FEATURE_KEY, shoppingCartReducer),
		EffectsModule.forFeature(ShoppingCartEffects),
	],
	exports: [ShoppingCartFeatureModule],
})
export class ShoppingCartModule {}
