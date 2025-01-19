import 'package:eprodaja_admin/models/proizvod.dart';
import 'package:eprodaja_admin/models/search_result.dart';
import 'package:eprodaja_admin/providers/product_provider.dart';

class LoggedProductProvider extends ProductProvider {
  @override
  Future<SearchResult<Proizvod>> get({filter}) {
    print("Im in logged product provider");

    return super.get(filter: filter);
  }
}
