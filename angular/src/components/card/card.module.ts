import { NgModule } from "@angular/core";
import { CardUiModule } from "./ui/card-ui.module";

@NgModule({
    imports: [CardUiModule],
    exports: [CardUiModule]
})
export class AppCardModule {}