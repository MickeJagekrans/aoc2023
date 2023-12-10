import scala.annotation.tailrec

val getHistory: List[Int] => List[Int] = 
  _.sliding(2).collect { case Seq(a, b) => b - a }.toList

def getHistories(prevList: List[Int]): List[List[Int]] =
  @tailrec def _getHistories(acc: List[List[Int]]): List[List[Int]] =
    val history = getHistory(acc.head)
    val newList = history +: acc

    if history.forall(_ == 0) then newList
    else _getHistories(newList)

  _getHistories.andThen(_.reverse)(prevList :: Nil)

val splitToInts: String => List[Int] = _.split(" ").map(_.toInt).toList

@main def main(): Unit =
  // Read all lines from file ../../9.input
  val lines = io.Source.fromFile("../../9.input").getLines.toArray
  val allHistories =
    lines
      .map(splitToInts)
      .map(getHistories andThen (_.map(_.reverse)))

  val res =
    allHistories
      .map(_.reduce((acc, history) => (acc.head + history.head) +: history).head)

  println(res.reduce(_ + _))