import { Component } from "@angular/core";
import { CategoriasService } from "./categorias.service";
import { CategoriesActions } from "./state/categorias.actions";

@Component({
	selector: "categorias-routing",
	template: `<router-outlet></router-outlet>`,
})
export class CategoriasRoutingComponent {
	constructor(private service: CategoriasService) {
		this.service.dispatch(CategoriesActions.initCategorias);
	}
}
