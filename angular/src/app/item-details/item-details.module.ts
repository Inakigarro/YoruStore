import { NgModule } from "@angular/core";
import { ItemDetailsComponent } from "./item-details.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";

@NgModule({
	declarations: [ItemDetailsComponent],
	imports: [CommonModule, MatCardModule],
	exports: [ItemDetailsComponent],
})
export class ItemDetailsModule {}
