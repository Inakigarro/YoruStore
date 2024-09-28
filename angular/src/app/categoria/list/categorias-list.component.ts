import { Component, OnDestroy, OnInit } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { Observable, Subject } from "rxjs";
import { Button, Item } from "@root/components/models";
import { CategoriesActions } from "../state/categorias.actions";

@Component({
	selector: "categorias-list",
	templateUrl: "./categorias-list.component.html",
	styleUrl: "categorias-list.component.scss",
})
export class CategoriasListComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public listTitle$: Observable<string>;
	public listData$: Observable<Item[]>;
	public buscarButton: Button = {
		type: "basic",
		icon: "search",
		label: "Buscar",
		action: CategoriesActions.searchButtonClicked(),
	};
	constructor(private service: CategoriasService) {}

	public ngOnInit(): void {
		this.listData$ = this.service.data$;
		this.listTitle$ = this.service.title$;
	}

	public ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}

	public onBuscarButtonClicked() {}
}
