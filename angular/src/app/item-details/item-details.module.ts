import { NgModule } from "@angular/core";
import { ItemDetailsComponent } from "./item-details.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { ItemDetailsRoutingModule } from "./item-details-routing.module";
import { AppToolbarModule } from "../../components/toolbar/toolbar.module";
import { AppButtonModule } from "../../components/button/button.module";

@NgModule({
	declarations: [ItemDetailsComponent],
	imports: [
		CommonModule,
		MatCardModule,
		ItemDetailsRoutingModule,
		AppToolbarModule,
		AppButtonModule,
	],
	exports: [ItemDetailsComponent],
})
export class ItemDetailsModule {}
