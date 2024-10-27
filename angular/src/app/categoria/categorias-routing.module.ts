import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CategoriasListComponent } from "./list/categorias-list.component";
import { CategoriasRoutingComponent } from "./categorias-routing.component";

const routes: Routes = [
	{
		path: "",
		component: CategoriasRoutingComponent,
		children: [
			{
				path: "",
				component: CategoriasListComponent,
			},
		],
	},
	{
		path: ":itemId",
		loadChildren: () =>
			import("../item-details/item-details.module").then(
				(m) => m.ItemDetailsModule
			),
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class CategoriasRoutingModule {}
