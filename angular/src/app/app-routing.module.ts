import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { MainComponent } from "./main/main.component";
import { AuthGuard } from "./auth/auth.guard";
import { LoginComponent } from "./auth/login/login.component";

export const routes: Routes = [
	{
		path: "",
		component: MainComponent,
		canActivate: [AuthGuard],
	},
	{
		path: "login",
		component: LoginComponent,
	},
	{
		path: ":nombreCategoria",
		loadChildren: () =>
			import("./categoria/categorias.module").then((m) => m.CategoriasModule),
		canActivate: [AuthGuard],
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule],
})
export class AppRoutingModule {}
