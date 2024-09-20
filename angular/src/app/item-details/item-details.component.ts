import { Component, Input, OnInit } from "@angular/core";
import { Item } from "@components/models";
import { ItemDetailsService } from "./item-details.service";
import { Observable } from "rxjs";

@Component({
	selector: "item-details",
	templateUrl: "item-details.component.html",
	styleUrl: "item-details.component.scss",
})
export class ItemDetailsComponent implements OnInit {
	public item$: Observable<Item | undefined>;

	constructor(private service: ItemDetailsService) {}

	public ngOnInit(): void {
		this.item$ = this.service.currentItem$;
	}
}
