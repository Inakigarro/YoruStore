import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import {
	getCurrentCategory,
	getCurrentCategoryName,
	getCurrentItems,
	getLoaded,
} from "./state/categorias.selectors";
import { WebApiService } from "../web-api.service";
import { NavigationService } from "../navigation.service";
import { filter } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class CategoriasService {
	public title$ = this.store.select(getCurrentCategoryName);
	public data$ = this.store.select(getCurrentItems);
	public currentCategoria$ = this.store.select(getCurrentCategory);
	public loaded$ = this.store.select(getLoaded);
	constructor(
		private store: Store,
		private navigationService: NavigationService,
		private webApi: WebApiService
	) {}

	public obtenerCategorias() {
		return this.webApi.obtenerCategorias().pipe(filter((x) => !!x));
	}

	public obtenerCategoria(id: string) {
		return this.webApi.obtenerCategoriaById(id);
	}

	public obtenerConFiltro(categoriaId: string, filter: string) {
		return this.webApi.obtenerItemsPorFiltro(categoriaId, filter);
	}

	public navigate(url: string[], isRelative: boolean = false) {
		this.navigationService.navigate(url, isRelative);
	}

	public dispatch(action: Action) {
		this.store.dispatch(action);
	}
}
