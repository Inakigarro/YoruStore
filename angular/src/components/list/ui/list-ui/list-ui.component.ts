import { Component, Input, OnInit } from "@angular/core";
import { Item } from "@root/components/models";
import { Observable } from "rxjs";

@Component({
	selector: "list-ui",
	templateUrl: "./list-ui.component.html",
	styleUrl: "./list-ui.component.scss",
})
export class ListUiComponent implements OnInit {
	@Input()
	public id: string;

	@Input()
	public data$: Observable<Item[]>;

	public ngOnInit(): void {
		this.data$.subscribe();
	}
}
