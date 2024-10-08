import { Component, OnDestroy, OnInit } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { delay, Observable, Subject } from "rxjs";
import { Button, Item } from "@root/components/models";
import { CategoriesActions } from "../state/categorias.actions";
import { createAction } from "@ngrx/store";

const dummyAction = createAction("");

@Component({
	selector: "categorias-list",
	templateUrl: "./categorias-list.component.html",
	styleUrl: "categorias-list.component.scss",
})
export class CategoriasListComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public listTitle$: Observable<string>;
	public listData$: Observable<Item[]>;
	public loaded$: Observable<boolean>;
	public buscarButton: Button = {
		type: "basic",
		icon: "search",
		label: "Buscar",
		action: dummyAction(),
	};
	constructor(private service: CategoriasService) {}

	public ngOnInit(): void {
		this.listData$ = this.service.data$;
		this.listTitle$ = this.service.title$;
		this.loaded$ = this.service.loaded$;
	}

	public ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}

	public onBuscarButtonClicked(value: string) {
		this.service.dispatch(CategoriesActions.searchButtonClicked({ value }));
	}
	public onClearButtonClicked(input: HTMLInputElement) {
		input.value = "";
		this.service.dispatch(CategoriesActions.searchClearButtonClicked());
	}

	public onInputKeyUp(value: string) {
		this.service.dispatch(CategoriesActions.searchInputKeyUp({ value: value }));
	}
}
