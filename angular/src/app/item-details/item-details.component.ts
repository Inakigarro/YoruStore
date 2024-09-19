import { Component, Input } from "@angular/core";
import { Item } from "@components/models";

@Component({
	selector: "item-details",
	templateUrl: "item-details.component.html",
	styleUrl: "item-details.component.scss",
})
export class ItemDetailsComponent {
	@Input()
	public item: Item;
}
