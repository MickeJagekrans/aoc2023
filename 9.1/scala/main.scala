import scala.annotation.tailrec

val getHistory: List[Int] => List[Int] = 
  _.sliding(2).collect { case Seq(a, b) => b - a }.toList

def getHistories(prevList: List[Int]): List[List[Int]] =
  @tailrec def _getHistories(acc: List[List[Int]]): List[List[Int]] =
    val history = getHistory(acc.head)
    val newList = history +: acc

    if history.forall(_ == 0) then newList
    else _getHistories(newList)

  _getHistories(prevList :: Nil)

val splitToInts: String => List[Int] = _.split(" ").map(_.toInt).toList
val reverseNestedLists: List[List[Int]] => List[List[Int]] = _.map(_.reverse)
val sumListHeads: (List[Int], List[Int]) => List[Int] = (a, b) => (a.head + b.head) +: b

@main def main(): Unit =
  // Read all lines from file ../../9.input
  val lines = io.Source.fromFile("../../9.input").getLines.toArray
  val result =
    lines
      .map(splitToInts andThen getHistories andThen reverseNestedLists)
      .map(_.reduce(sumListHeads).head)
      .reduce(_ + _)

  println(result)