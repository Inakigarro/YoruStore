import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { MainComponent } from "./main/main.component";

export const routes: Routes = [
	{
		path: "",
		component: MainComponent,
	},
	{
		path: "medias",
		loadChildren: () =>
			import("./categoria/categorias.module").then((m) => m.CategoriasModule),
	},
	{
		path: "pantalones",
		loadChildren: () =>
			import("./categoria/categorias.module").then((m) => m.CategoriasModule),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
