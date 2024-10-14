import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./auth/login/login.component";

export const routes: Routes = [
	{
		path: "",
		children: [],
	},
	{
		path: "login",
		component: LoginComponent,
	},
	{
		path: ":nombreCategoria",
		loadChildren: () =>
			import("./categoria/categorias.module").then((m) => m.CategoriasModule),
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
