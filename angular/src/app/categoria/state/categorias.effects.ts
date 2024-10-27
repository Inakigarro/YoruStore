import { Injectable } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { filter, map, switchMap, tap } from "rxjs";
import { DetailsButtonClicked } from "@root/components/card/state/card.actions";
import { CategoriesActions } from "./categorias.actions";
import { RouterService } from "@root/app/router/router.service";
import { fetch } from "@ngrx/router-store/data-persistence";

@Injectable()
export class CategoriesEffects {
	public initCategorias$ = createEffect(() =>
		this.actions.pipe(
			ofType(CategoriesActions.initCategorias),
			switchMap(() => this.routerService.routerParams$),
			filter((params) => !!params["nombreCategoria"]),
			fetch({
				run: (params) =>
					this.service
						.obtenerCategoriaPorNombre(params["nombreCategoria"])
						.pipe(
							filter((x) => !!x),
							map((categoria) =>
								CategoriesActions.categoriaCargada({
									categoria: categoria,
								})
							)
						),
				onError: (params, error) =>
					CategoriesActions.categoriaCargada({
						categoria: { id: "", nombre: "", items: [] },
						error: error,
					}),
			})
		)
	);
	public itemDetailsButtonClicked$ = createEffect(
		() =>
			this.actions.pipe(
				ofType(DetailsButtonClicked),
				tap(({ itemId }) => this.service.navigate([`${itemId}`], true))
			),
		{ dispatch: false }
	);
	constructor(
		private readonly actions: Actions,
		private service: CategoriasService,
		private routerService: RouterService
	) {}
}
