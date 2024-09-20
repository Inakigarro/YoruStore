import { NgModule } from "@angular/core";
import { ItemDetailsComponent } from "./item-details.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { ItemDetailsRoutingModule } from "./item-details-routing.module";

@NgModule({
	declarations: [ItemDetailsComponent],
	imports: [CommonModule, MatCardModule, ItemDetailsRoutingModule],
	exports: [ItemDetailsComponent],
})
export class ItemDetailsModule {}
