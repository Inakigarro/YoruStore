import { Injectable } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { SecondaryToolbarActions } from "@root/app/state/app.actions";
import { filter, map, switchMap, tap, withLatestFrom } from "rxjs";
import { CategoriesActions } from "./categorias.actions";

@Injectable()
export class CategoriesEffects {
	public CategoryButtonClicked$ = createEffect(() =>
		this.actions.pipe(
			ofType(SecondaryToolbarActions.categoryButtonClicked),
			switchMap((action) => this.service.obtenerCategoria(action.categoriaId)),
			filter((x) => !!x),
			tap((categoria) =>
				this.service.navigate([`${categoria.nombre.toLowerCase()}`])
			),
			map((categoria) => CategoriesActions.categoriaCargada({ categoria }))
		)
	);

	public SearchButtonClicked$ = createEffect(() =>
		this.actions.pipe(
			ofType(CategoriesActions.searchButtonClicked),
			filter((action) => action.value.length > 0),
			withLatestFrom(this.service.currentCategoria$),
			switchMap(([action, categoria]) =>
				this.service.obtenerConFiltro(categoria?.id!, action.value)
			),
			filter((x) => !!x),
			map((result) => CategoriesActions.itemFiltered({ items: result }))
		)
	);

	public ClearSearchButtonClicked$ = createEffect(() =>
		this.actions.pipe(
			ofType(CategoriesActions.searchClearButtonClicked),
			withLatestFrom(this.service.currentCategoria$),
			switchMap(([_, categoria]) =>
				this.service.obtenerCategoria(categoria?.id!)
			),
			filter((x) => !!x),
			map((categoria) => CategoriesActions.categoriaCargada({ categoria }))
		)
	);

	constructor(
		private readonly actions: Actions,
		private service: CategoriasService
	) {}
}
