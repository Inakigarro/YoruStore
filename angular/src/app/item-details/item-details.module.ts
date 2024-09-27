import { NgModule } from "@angular/core";
import { ItemDetailsComponent } from "./item-details.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { ItemDetailsRoutingModule } from "./item-details-routing.module";
import { AppToolbarModule } from "../../components/toolbar/toolbar.module";
import { AppButtonModule } from "../../components/button/button.module";
import { EffectsModule } from "@ngrx/effects";
import { ItemDetailsEffects } from "./state/item-details.effects";
import { StoreModule } from "@ngrx/store";
import {
	ITEM_DETAILS_FEATURE,
	itemDetailsReducer,
} from "./state/item-details.reducer";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";

@NgModule({
	declarations: [ItemDetailsComponent],
	imports: [
		CommonModule,
		MatCardModule,
		ItemDetailsRoutingModule,
		AppToolbarModule,
		AppButtonModule,
		MatProgressSpinnerModule,
		StoreModule.forFeature(ITEM_DETAILS_FEATURE, itemDetailsReducer),
		EffectsModule.forFeature(ItemDetailsEffects),
	],
	exports: [ItemDetailsComponent],
})
export class ItemDetailsModule {}
