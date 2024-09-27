import { Injectable } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { SecondaryToolbarActions } from "@root/app/state/app.actions";
import { filter, map, switchMap, tap } from "rxjs";
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
	constructor(
		private readonly actions: Actions,
		private service: CategoriasService
	) {}
}
