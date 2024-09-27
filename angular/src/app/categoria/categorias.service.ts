import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import {
	getCurrentCategoryItems,
	getCurrentCategoryName,
} from "./state/categorias.selectors";
import { WebApiService } from "../web-api.service";
import { NavigationService } from "../navigation.service";

@Injectable({
	providedIn: "root",
})
export class CategoriasService {
	public title$ = this.store.select(getCurrentCategoryName);
	public data$ = this.store.select(getCurrentCategoryItems);
	constructor(
		private store: Store,
		private navigationService: NavigationService,
		private webApi: WebApiService
	) {}

	public obtenerCategoria(id: string) {
		return this.webApi.obtenerCategoriaById(id);
	}

	public navigate(url: string[], isRelative: boolean = false) {
		this.navigationService.navigate(url, isRelative);
	}
}
