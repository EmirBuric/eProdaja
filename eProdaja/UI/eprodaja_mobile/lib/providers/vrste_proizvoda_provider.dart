import 'package:eprodaja_mobile/models/vrste_proizvoda.dart';
import 'package:eprodaja_mobile/providers/base_provider.dart';

class VrsteProizvodaProvider extends BaseProvider<VrsteProizvoda> {
  VrsteProizvodaProvider() : super("VrsteProizvoda");

  @override
  VrsteProizvoda fromJson(data) {
    // TODO: implement fromJson
    return VrsteProizvoda.fromJson(data);
  }
}
