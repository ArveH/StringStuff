# StringStuff
Calculating the Damerau Levenshtein Distance. 

Code is based on the Wikipedia article:
https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
There are three versions of the algorithm:
- Optimal string alignment distance (restricted edit distance)
- Levenshtein Distance (same as above, but without transposition)
- True Damerau–Levenshtein distance with adjacent transpositions

I also added a version found on StackOverflow:
http://stackoverflow.com/questions/9453731/how-to-calculate-distance-similarity-measure-of-given-2-strings/9454016#945401

It's a Console program where you enter two strings, and it will print the distance using all 4 versions of the algorithm. It will also print the internal matrix used when calculating the distance.

