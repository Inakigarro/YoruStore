import { Component, OnInit } from "@angular/core";
import { AppService } from "../app.service";
import { backdropClicked } from "../state/app.actions";
import { Observable } from "rxjs";
import { Item } from "@components/models";

@Component({
	selector: "app-main",
	templateUrl: "./main.component.html",
})
export class MainComponent implements OnInit {
	public isShoppingCartOpened$: Observable<boolean>;
	public data$: Observable<Item[]>;
	public listTitle$: Observable<string>;
	public loading$: Observable<boolean>;
	constructor(private service: AppService) {}
	public ngOnInit(): void {
		this.isShoppingCartOpened$ = this.service.isShoppingCartOpened$;
		this.data$ = this.service.currentCategoryItems$;
		this.listTitle$ = this.service.currentCategoryName$;
		this.loading$ = this.service.loading$;
	}
	public onBackdropClicked() {
		this.service.dispatch(backdropClicked());
	}
}
