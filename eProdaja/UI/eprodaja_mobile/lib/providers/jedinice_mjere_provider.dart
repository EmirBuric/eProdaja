
import 'package:eprodaja_mobile/models/jedinice_mjere.dart';
import 'package:eprodaja_mobile/providers/base_provider.dart';

class JediniceMjereProvider extends BaseProvider<JediniceMjere> {
  JediniceMjereProvider() : super("JediniceMjere");

  @override
  JediniceMjere fromJson(data) {
    // TODO: implement fromJson
    return JediniceMjere.fromJson(data);
  }
}
